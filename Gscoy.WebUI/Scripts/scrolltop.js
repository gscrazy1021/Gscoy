$(document).ready(function () {
    //���Ƚ�#back-to-top����
    $("#back-to-top").hide();
    //����������λ�ô��ھඥ��100��������ʱ����ת���ӳ��֣�������ʧ
    $(function () {
        $(window).scroll(function () {
            if ($(window).scrollTop() > 100) {
                $("#back-to-top").fadeIn(500);
            }
            else {
                $("#back-to-top").fadeOut(500);
            }
        });
        //�������ת���Ӻ󣬻ص�ҳ�涥��λ��
        $("#back-to-top").click(function () {
            $('body,html').animate({ scrollTop: 0 }, 100);
            return false;
        });
    });
});