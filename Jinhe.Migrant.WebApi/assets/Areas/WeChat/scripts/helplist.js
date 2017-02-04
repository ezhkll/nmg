var addtip = new Vue({
    el: "#addtip",
    data: {
        loading: false
    }
});

var helps = new Vue({
    el: "#helplist",
    data: {
        helps: [],
        page: 0,
        listnum: 5,
        status:2
    },
    ready: function () {


        var tmp = this;
        tmp.getdata();
        tmp.pagedown();
       

    },
    methods: {
        tabclick: function (event) {
            $(event.currentTarget).addClass('active').siblings().removeClass('active');

            var tmp = this;
            tmp.helps = [];
            tmp.page = 0;
            tmp.status = $(event.currentTarget).data('type');
            tmp.getdata();
            tmp.pagedown();
        },
        pagedown: function () {
            var tmp = this;

            $("body").infinite(10).on("infinite", function () {
                console.log(addtip.loading);

                if (addtip.loading == true) { return; }
                addtip.loading = true;

                tmp.page++;
                tmp.getdata();
            });
        },
        getdata: function () {
            var tmp = this;

            $.ajax({
                url: config.ajaxurl + 'Enventall',
                data: {
                    type: 2,
                    start: tmp.page*tmp.listnum,
                    length: tmp.listnum
                },
                async:false,
                type: 'GET',
                dataType: 'JSON',
                contentType: 'application/json',
                success: function (data) {
                    
                    //如果获取不到数据取消下拉加载
                    if (data.Data.Items.length==0){
                        $("body").destroyInfinite();
                        addtip.loading = false;
                        return;
                    }

                    //转换图片组
                    for (i in data.Data.Items) {
                        
                            if (data.Data.Items[i].Startpicurl == null)
                            { data.Data.Items[i].Startpicurl = []; }
                            else
                            { data.Data.Items[i].Startpicurl = data.Data.Items[i].Startpicurl.split(','); }

                            tmp.helps.push(data.Data.Items[i]);

                        
                    }
                    


                    addtip.loading = false;

                },
                error: function (data) {
                    addtip.loading = false;
                }

            });
            

            
        }

    }

});

