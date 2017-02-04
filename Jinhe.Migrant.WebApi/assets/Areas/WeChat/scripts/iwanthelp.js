var help = new Vue({
    el: "#help",
    data: {
        telb: '',
        mszs: 0,
        xnum: 0,
        inum:3,
        wximages: [],
        log: '',
        gpsx: '',
        gpsy: '',
        Basicgrid:'',
        address:''
    },
    ready: function () {
        //initWxApi();
        $.ajax({
            url: config.ajaxurl + 'SDistrict/'+config.grid,
            type: 'GET',
            dataType: 'JSON',
            contentType: 'application/json',
            success: function (data2) {
                data = data2.Data;
                var qxarr = [];
                for (i in data)
                {
                    qxarr.push({title:data[i].Dwmc,value:data[i].Gbbh});
                }
                console.log(qxarr[0]);
                $("input[name='Basicgridb']").val(qxarr[0].title);
                help.Basicgrid=qxarr[0].value;
                $("input[name='Basicgridb']").select({
                    title: '区县(街道)',
                    items: qxarr,
                    onChange: function () {
                        console.log('select');
                        help.Basicgrid = $("input[name='Basicgridb']").attr('data-values');
                    }
                });

            },
            error: function () { }
        })

       
        wx.ready(function () {
            help.dw();
        });
    },
    methods: {
        //字数统计
        zstj: function (event) {
            this.mszs = event.currentTarget.value.length;
        },
        //手机号码限制11位
        phonesli: function (event) {
            event.currentTarget.value = event.currentTarget.value.slice(0, 11);
        },
        //定位
        dw: function () {
            wx.getLocation({
                type: 'wgs84', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
                success: function (res) {
                    help.gpsy = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                    help.gpsx = res.longitude; // 经度，浮点数，范围为180 ~ -180。
                    //var speed = res.speed; // 速度，以米/每秒计
                    //var accuracy = res.accuracy; // 位置精度
                    console.log(help.gpsx, help.gpsy);
                    $.ajax({
                        url: "http://restapi.amap.com/v3/geocode/regeo?location=" + help.gpsx + "," + help.gpsy + "&key=" + config.mapkey,
                        type: "GET",
                        dataType: "json",
                        contentType: 'application/json',
                        data: "",
                        success: function (data) {
                            
                            console.log(data);
                            help.address = data.regeocode.formatted_address;
                            
                        },
                        error: function (data) {
                            console.log(data);
                        },
                    });
                },
            });
        },
        //选定区县
        qx: function (event) {
            console.log('select');
            help.Basicgrid = $(event.currentTarget).attr('data-values');
        },
        //上传图片
        uploadimage: function () {
            if (help.wximages.length >= help.inum) {  return; }
            //start
            wx.chooseImage({
                count: help.inum - help.wximages.length, // 默认9
                sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
                sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
                success: function (res) {
                    var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片
                    if (localIds) {
                        var serverIds = new Array();
                        var fi = 0;
                        for (var i = 0; i < localIds.length; i++) {
                            help.log+='本地' + localIds[i]+'<br/>';

                            //手机内存不足会导致这里中断
                            wx.uploadImage({
                                localId: localIds[i], // 需要上传的图片的本地ID，由chooseImage接口获得
                                isShowProgressTips: 9, // 默认为1，显示进度提示
                                success: function (res) {
                                    help.log+='服务器' + res.serverId+'<br/>';
                                    fi++;
                                    serverIds.push(res.serverId); // 返回图片的服务器端ID
                                    if (fi == localIds.length) {
                                        serverIdstxt = serverIds.join(',');
                                        $.ajax({
                                            url: config.wxupload,
                                            type: 'get',
                                            data: { mediaid:serverIdstxt },
                                            dataType: 'json',
                                            contentType: 'application/json',
                                            success: function (res) {
                                                help.wximages = help.wximages.concat(res.Data);
                                                $("input[name='PicUrls']").val(help.wximages.join(','));
                                                help.log = $("input[name='PicUrls']").val();
                                                if (help.wximages.length >= help.inum) { $('#jiatub').hide(); }
                                            },
                                            error: function () {
                                                console.log('error');
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }
                }
            });
            //end
        },

        //提交
        submit: function () {
            if ($('input[name="Publicreporter"]').val() == '') { $.toptip('请输入姓名', 'error'); return; }
            if ($('input[name="Callbacknumber"]').val() == '') { $.toptip('请输入手机号码', 'error'); return; }
            if ($('textarea[name="Eventdesc"]').val() == '') { $.toptip('请输入问题描述', 'error'); return; }
            var desctxt=$('textarea[name="Eventdesc"]').val();
            $('textarea[name="Eventdesc"]').val('微信反映/' + desctxt);

            $.showLoading();
            console.log($('#helpform').serializeObject());
            console.log(JSON.stringify($('#helpform').serializeObject()));

            $.ajax({
                url: config.ajaxurl + 'BEvent/',
                type: 'POST',
                dataType: 'JSON',
                contentType: 'application/json',
                async: false,
                data: JSON.stringify($('#helpform').serializeObject()),
                success: function (data) {
                    console.log(data);
                    $.hideLoading();
                    switch (data.Code)
                    {
                        case 0:
                            $.toast("提交成功");
                            setTimeout(function () {
                                location.href = 'Index?r=' + rnd();
                            }, 2000);
                            break;
                        case 9001:
                            $.toptip(data.Message, 'error');
                            break;
                        default:
                            $.toptip('未知错误', 'error');
                            break;
                    }
                    
                },
                error: function () {
                    $.hideLoading();
                    $.toast("提交失败", "cancel");
                }
            });
            
        }

        //methods end
    }

});