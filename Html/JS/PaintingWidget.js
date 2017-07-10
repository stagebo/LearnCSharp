/*
*painting plugin 1.0   2017-4-21 by stagebo
*/
if (!jQuery||jQuery === undefined) {
    alert("绘画插件需要jQuery插件！请查看页面");
}
(function () {
    /**
     *function                 绘制线段
     *options       json       参数列表  包括以下内容
     *startX        int        起始位置x坐标
     *startY        int        起始位置y坐标
     *endX          int        终止位置x坐标
     *endY          int        终止位置y坐标
     *lineWidth     int        线宽
     *lineColor     string     线条颜色
     */
    $.fn.drawLine = function (options) {
        var settings = {
            "startX": 0
            , "startY": 0
            , "endX": 0
            , "endY": 0
            , "lineWidth": 1
            , "lineColor": "#000000"
            , "": ""
            , "": ""
        }
        var obj = $(this);
        settings = $.extend(settings, options);
        var sx = settings['startX'], sy = settings['startY'], ex = settings['endX'], ey = settings['endY'];
        var mx = (sx + ex) / 2, my = (sy + ey) / 2;
        var lw = Math.sqrt(Math.pow(sy - ey, 2) + Math.pow(sx - ex, 2));
        var ssx = mx - lw / 2 + 9, ssy = my - settings['lineWidth'] / 2 + 9;
        var deg = Math.atan((ey - sy) / (ex - sx)) * 180 / Math.PI;
        console.log("deg:" + deg + "--sy:" + ssy + "--ssx:" + ssx);
        var style = 'width:' + lw + 'px;height:' + settings['lineWidth'] + 'px;background-color:' + settings['lineColor']
            + ';top:' + ssy + 'px;left:' + ssx + 'px;'
            + ' transform:rotate(' + deg + 'deg);';
        var line = $("<div></div>").addClass("position-absolute").attr('style', style);
        obj.append(line);
    }
    /**
     * function                         绘制圆形
     * params       options   json      数列表，包括以下内容
     * params       ox        int       圆心位置x坐标
     * params       oy        int       圆心位置y坐标
     * params       radius    int       圆半径
     * params       lineColor string    圆线条颜色
     * params       fullColor string    圆内部填充颜色
     */
    $.fn.drawCircle = function (options) {
        var opts = {
            "ox": 10,
            "oy": 10,
            "radius": 10,
            "lineColor": '#f00',
            "fullColor": '#fff',
        }
        opts = $.extend(opts, options);
        var r = opts['radius'], ox = opts['ox'], oy = opts['oy'];
        var style = 'width:' + r * 2 + 'px;height:' + r * 2 + 'px;border-radius:' + r + 'px;'
                    + 'left:' + (ox - r+8) + 'px;top:' + (oy - r+8) + 'px;'
                    + 'background-color:' + opts['fullColor'] + ';border:solid;border-color:' + opts['lineColor'] + ';';
        var circle = $("<div></div>").addClass("position-absolute").attr('style', style);
        $(this).append(circle);
    }
    /**
     *function 绘制椭圆
     *
     */
    $.fn.drawEllipse = function (options) {

    }



})(jQuery);