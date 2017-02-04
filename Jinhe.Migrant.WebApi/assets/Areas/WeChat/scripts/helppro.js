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
        status: 2,
        no:-1
    },
    ready: function () {


        var tmp = this;
        tmp.getdata();
        tmp.pagedown();
       

    },
    methods: {
        //标签点击
        tabclick: function (event) {
            var tmp = this;
            tmp.helps = [];
            tmp.page = 0;
            tmp.status = $(event.currentTarget).data('type');
            tmp.getdata();
            tmp.pagedown();
        },

        //项目点击
        helpclick: function (event) {
            var tmp = this;
            var nowno=$(event.currentTarget).data('no');

            //console.log(tmp.no,this.no,nowno);
            //如果点击的项目和之前保存的已展开的项目编号相同，变为-1收起项目
            if (tmp.no == nowno)
            { tmp.no = -1; }
            else
                //改变项目编号，展开另一处
            { tmp.no = nowno;}

            
        },

        seepic: function (event) {
            seepic(event);
        },

        //下拉加载
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

        //更新数据
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
                    
                    $('.clicktog').toggle(function () {
                        console.log($(this).find('.infotog')[0]);
                        $(this).find('.infotog').show();
                    }, function () { $(this).find('.infotog').hide(); });

                    addtip.loading = false;

                },
                error: function (data) {
                    addtip.loading = false;
                }

            });
            

            
        }

    }

});

