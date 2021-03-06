﻿layui.use(['layer', 'jquery'], function () {
    var $ = layui.jquery;
    var layer = layui.layer;
    var checkBox = $('#checkCofirm');
    var btnCofirm = $('#btnConfirm');
    var count = 14;
    
    var server_msg = $("#server_msg").val();
    if (server_msg.length > 0) {
        layer.alert(server_msg);
        return;
    }
    //控制时间显示
    var changeCheck = setInterval(function () {
        btnCofirm.val(count + ' 秒');
        if (count < 1)
        {
            clearInterval(changeCheck);
            btnCofirm.val('确 定').attr('class', 'btnConfirm');
            btnCofirm.removeAttr('disabled');
        }
        count--;
    }, 1000);

    var tip = $("#tip");
    $(window).scroll(function () {
        if ($(document).scrollTop() >= $(document).height() - $(window).height()) {
            tip.attr('style', 'display:none');
        }else {
            tip.attr('style', 'display:block');
        }
    });
        


});