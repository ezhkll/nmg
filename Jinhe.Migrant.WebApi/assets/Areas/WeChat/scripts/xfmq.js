var news = new Vue({
    el: "#xfmq",
    data: {},
    ready: function () {
        $('right-panel').height($(window).height());
        $('right-panel-info').height($(window).height() - 45);

    },
    methods: {
        seexfmq: function (event) {
            $.ajax({
                url: config.fakerdata,
                typr: 'GET',
                dataType: 'json',
                async: true,
                success: function (data) {
                    right.news = data.articlehtml[0];
                    right.bar = true;
                    $('#right').removeClass('mr').addClass('ml');

                },
                error: function () {
                    console.log('error');
                }
            });

            //

        }
    }
});

var right = new Vue({
    el: "#right",
    data: {
        bar: false,
        news: {}
    },
    ready: function () { },
    methods: {
        close: function () {
            $('#right').removeClass('ml').addClass('mr');
            right.bar = false;
            right.news = {};
        }
    }
});