var addtip = new Vue({
    el: "#addtip",
    data: {
        loading: false
    }
});

var news = new Vue({
    el: "#news",
    data: {
        page:0
    },
    ready: function () {
        var news = this;

        $('right-panel').height($(window).height());
        $('right-panel-info').height($(window).height() - 45);

        
        $("body").infinite(10).on("infinite", function () {
            console.log(addtip.loading);

           if (addtip.loading==true) { return; }
            addtip.loading = true;
           //ajax

            console.log('执行');
            setTimeout(function () {
                $("#news").append("<p> 我是新加载的内容 </p>");
                news.page++;
                if (news.page > 5) { $("body").destroyInfinite(); }
                addtip.loading = false;
            }, 1500);
        });
    },
    methods: {
        seenews: function (event) {
           

            var dom = event.currentTarget;
            $.ajax({
                url: config.fakerdata,
                typr:'GET',
                dataType: 'json',
                async:true,
                success: function (data) {
                    right.news = data.articlehtml[$(dom).attr('num')];
                    right.bar = true;
                    $('#right').show();
                    window.history.pushState('', '', window.location.href);
                    $(window).on('popstate', function () {
                       
                        right.close(2);
                    });
                },
                error: function () {
                    console.log('error');
                }
            });

            //
            
        },
        loadhtml: function () {
            alert('1');
        }
    }
});



var right = new Vue({
    el: "#right",
    data: {
        bar:false,
        news: {}
    },
    ready: function () {
       
    },
    methods: {
        
        close: function (back) {
            if (back != 2)
            { history.go(-1);}
            $('#right').hide();
            right.bar = false;
            right.news = {};
        }
    }
});