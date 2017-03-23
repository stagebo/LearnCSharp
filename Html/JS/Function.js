function doNotClickMe() {
    $("body").hide();
    alert("~");
    setTimeout("reSet()", 3000);
}
function reSet() {
    $("body").show();
}
/**
  *格式化json字符串
  *方便观察
  */
function formatJsonString(json) {
    if (typeof json !== 'string') {
        json = JSON.stringify(json);
    } else {
        json = JSON.parse(json);
        json = JSON.stringify(json);
    }
    // 在大括号前后添加换行
    var reg = /([\{\}])/g;
    json = json.replace(reg, '\r\n$1\r\n');
    // 中括号前后添加换行
    reg = /([\[\]])/g;
    json = json.replace(reg, '\r\n$1\r\n');
    // 逗号后面添加换行
    reg = /(\,)/g;
    json = json.replace(reg, '$1\r\n');

    // 去除多余的换行
    reg = /(\r\n\r\n)/g;
    json = json.replace(reg, '\r\n');
    // 逗号前面的换行去掉
    reg = /\r\n\,/g;
    json = json.replace(reg, ',');
    //冒号前面缩进
    reg = /\:/g;
    json = json.replace(reg, ': ');


    return null;
}

/**
  *该方法将json变成格式化字符串，方便观察
  *params：json--合格的json
  *return：json转换后的字符串，可以直接输出观察
  */
var formatToJsonString = function (json) {
    if (typeof json != 'string') {
        json = JSON.stringify(json);
    } else {
        json = JSON.parse(json);
        json = JSON.stringify(json);
    }
    //添加回车
    json = json//.replace(/\\\"/g, "")
            .replace(/",/g, "\",\n")
            .replace(/null,/g, "null,\n")
            .replace(/},/g, "},\n")
            .replace(/{/g, "{\n")
            .replace(/:{/g, ":\n{")
            .replace(/\],/g, "],\n")
            .replace(/},/g, "\n},")
            .replace(/:\[/g, ":\n\[\n")
            .replace(/}\]}/g, "\n}\n]\n}\n")
            .replace(/}\]/g, "\n}\n]")
            .replace(/"}/g, "\"\n}")
            .replace(/\[{/g, "[\n{");
    //添加缩进
    var arr = json.split("\n");
    var len = 0;
    var result = "";
    for (var i = 0; i < arr.length; i++) {
        /*先判断}和]*/
        if (arr[i].indexOf("}") >= 0) {
            if (len >= 0) {
                len = len - 1;
            }
        }
        if (arr[i].indexOf("\]") >= 0) {
            if (len >= 0) {
                len = len - 1;
            }
        }
        var padding = "";
        var PADDING = "    ";
        for (var k = 0; k < len; k++) {
            padding = padding + PADDING;
        }
        arr[i] = padding + arr[i] + "\n";
        result += arr[i];
        /*再判断[和{*/
        if (arr[i].indexOf("{") >= 0)
            len = len + 1;
        if (arr[i].indexOf("\[") >= 0)
            len = len + 1;
    }

    return result;
}