/**
 * bool--表单Post提交方式
 * @param {any} url 地址
 * @param {any} handler 处理方法
 * @param {any} data 数据
 */
function CzbPost(url, handler, data) {
    let res;
    $.ajax({
        type: "post",
        url: url + "/?handler=" + handler,
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        data: data,
        async: false,
        success: (response) => {
            console.log(response);
            res = response;
        }

    });
    return res;
}
/**
 * void--清除标签id的value
 * @param {any} htmlid 标签id
 */
function Clear(htmlid) {
    htmlid = htmlid || "isExistID";
    $('#' + htmlid).html("");
}
/**
 * void--判断标签dataid的value是否存在数据库
 * @param {any} url 地址
 * @param {any} handler 处理方法
 * @param {any} htmldataid 数据标签id
 * @param {any} htmlid 显示返回值的标签id
 */
function isExistID(url, handler, htmldataid, htmlid) {
    htmlid = htmlid || "isExistID";
    $.get(url + '/?handler=' + handler, { ID: $("#" + htmldataid).val() }, function (data) {
        $('#' + htmlid).html(data);
    });
}
/**
 * void--查找函数(通过名称)
 * @param {any} url 跳转的地址
 * @param {any} htmlid 标签id
 */
function SearchByName(url, htmlid) {
    let data = $('#' + htmlid).val();
    window.location.href =url + data;
}
/**
 * void--添加函数
 * @param {any} url 地址
 * @param {any} handler 处理方法
 * @param {any} data 数据
 */
function Insert(url, handler, data) {
    if (CzbPost(url, handler, data)) {
        alert("添加成功");
        window.location.href = window.location.href;
    }
    else {
        alert("添加失败,请重试");
    }
}
/**
 * void--编辑函数
 * @param {any} url 地址
 * @param {any} handler 处理方法
 * @param {any} htmlid 标签主键id
 */
function Edit(url, handler, htmlid) {
    $.get(url + '/?handler=' + handler, { ID: htmlid }, function (data) {
        document.getElementById('Edit-body').innerHTML = template("tpl-Edit", data);
    });
}
/**
 * void--更新函数
 * @param {any} url 地址
 * @param {any} hanlder 处理方法
 * @param {any} data 数据
 */
function Update(url, hanlder, data) {
    if (CzbPost(url, hanlder, data)) {
        alert("更新成功");
        window.location.href = window.location.href;
    }
    else alert("更新失败,请重试");
}
/**
 * void--删除函数
 * @param {any} url 地址
 * @param {any} handler 处理方法
 * @param {any} htmlid 标签主键id
 */
function Delete(url,handler,htmlid) {
    if (confirm("确定删除吗?")) {
        $.get(url + "/?handler=" + handler, { ID: htmlid }, function (data) {
            if (data) {
                alert("删除成功");
                let nowurl = window.location.href;
                $.get(url + "/?handler=DeleteEmpty", { name: $('#search').val(), url: nowurl }, function (x) {
                    if (!/^\d+$/.test(x)) {
                        window.location.href = x;
                    } else {
                        let tempurl = nowurl.substr(0, nowurl.lastIndexOf('=')) + "=" + x;
                        window.location.href = tempurl;
                    }
                });

            }
            else {
                alert("删除失败,请重试");
            }
        });
    }
}