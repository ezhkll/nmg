
var config = {
    ajaxurl: 'http://api.gj.demo.jinhesoft.com/api/',
    wxupload: '/WeChat/file',
    areaname: '佳山乡',
    grid:'340504',
    mapkey: 'd9dafc513f4ac6e71e2430ee750f4d06',
    fakerdata: '/assets/Areas/WeChat/json/data.json'
}

function rnd() {
    return Math.round(Math.random() * 100000);
}

function seepic(event) {
    //拦截返回键
    window.history.pushState('', '', window.location.href);

    var sw = $(window).width();  //屏幕宽度
    var sh = $(window).height();  //屏幕高度
    var sb = sw / sh;   //屏幕宽高比

    var img = new Image();
    if ($(event.currentTarget)[0].style.backgroundImage.indexOf('"') == -1)
    { img.src = $(event.currentTarget)[0].style.backgroundImage.slice(4, -1); }
    else
    { img.src = $(event.currentTarget)[0].style.backgroundImage.slice(5, -2); }
    var imgw = img.width;  //图像宽
    var imgh = img.height;  //图像高
    var imgb = imgw / imgh; //图像宽高比

    
    
    if ($('.fc').length <= 0)
    { $('body').prepend('<div class="fc"></div>'); }

    //显示浮层，插入图片，绑定关闭事件
    $('.fc').css({ 'height': sh + 'px' }).show().html('<img src="'+img.src+'" />').on('click', function () {
        history.go(-1);
        $('.fc').hide();
    });

    $(window).on('popstate', function () {
        $('.fc').hide();
    });

    if (imgb > sb)
    {
        $('.fc img').css({ 'width': '100%', 'margin-top': ((sh - sw / imgb) / 2) + 'px' });
    }
    else
    { $('.fc img').css({ 'height': '100%' });}
    
    

}

/**
 * jQuery serializeObject
 * @copyright 2014, macek <paulmacek@gmail.com>
 * @link https://github.com/macek/jquery-serialize-object
 * @license BSD
 * @version 2.5.0
 */
!function (e, i) { if ("function" == typeof define && define.amd) define(["exports", "jquery"], function (e, r) { return i(e, r) }); else if ("undefined" != typeof exports) { var r = require("jquery"); i(exports, r) } else i(e, e.jQuery || e.Zepto || e.ender || e.$) }(this, function (e, i) { function r(e, r) { function n(e, i, r) { return e[i] = r, e } function a(e, i) { for (var r, a = e.match(t.key) ; void 0 !== (r = a.pop()) ;) if (t.push.test(r)) { var u = s(e.replace(/\[\]$/, "")); i = n([], u, i) } else t.fixed.test(r) ? i = n([], r, i) : t.named.test(r) && (i = n({}, r, i)); return i } function s(e) { return void 0 === h[e] && (h[e] = 0), h[e]++ } function u(e) { switch (i('[name="' + e.name + '"]', r).attr("type")) { case "checkbox": return "on" === e.value ? !0 : e.value; default: return e.value } } function f(i) { if (!t.validate.test(i.name)) return this; var r = a(i.name, u(i)); return l = e.extend(!0, l, r), this } function d(i) { if (!e.isArray(i)) throw new Error("formSerializer.addPairs expects an Array"); for (var r = 0, t = i.length; t > r; r++) this.addPair(i[r]); return this } function o() { return l } function c() { return JSON.stringify(o()) } var l = {}, h = {}; this.addPair = f, this.addPairs = d, this.serialize = o, this.serializeJSON = c } var t = { validate: /^[a-z_][a-z0-9_]*(?:\[(?:\d*|[a-z0-9_]+)\])*$/i, key: /[a-z0-9_]+|(?=\[\])/gi, push: /^$/, fixed: /^\d+$/, named: /^[a-z0-9_]+$/i }; return r.patterns = t, r.serializeObject = function () { return new r(i, this).addPairs(this.serializeArray()).serialize() }, r.serializeJSON = function () { return new r(i, this).addPairs(this.serializeArray()).serializeJSON() }, "undefined" != typeof i.fn && (i.fn.serializeObject = r.serializeObject, i.fn.serializeJSON = r.serializeJSON), e.FormSerializer = r, r });
