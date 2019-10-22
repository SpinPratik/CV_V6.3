function LoadGitter(userId, title, info, imageinfo, sticktype, sticktime, classname, StickNoteType) {
    try {
        switch (StickNoteType) {
            case 'autofade':
                var unique_id = $.gritter.add({
                    title: title,
                    text: info,
                    image: imageinfo,
                    sticky: false,
                    time: sticktime,
                    class_name: classname
                });
                break;
            case 'sticky': var unique_id = $.gritter.add({
                title: title,
                text: info,
                image: imageinfo,
                sticky: true,
                time: sticktime,
                class_name: classname
            });

                break;
            case 'noimage':
                $.gritter.add({
                    title: title,
                    text: info,
                    sticky: true,
                    time: sticktime
                });
                break;
            case 'callback':
                $.gritter.add({
                    title: title,
                    text: info,
                    sticky: sticktype,
                    before_open: function () {
                    },
                    after_open: function (e) {
                    },
                    before_close: function (e) {
                    },
                    after_close: function () {
                        PageMethods.GetReply(userId, title, info, OnGitterLoadSuccess, OnGitterLoadFailed);
                    }
                });
                break;
        }
    } catch (Msg) {
    }
}

function OnGitterLoadSuccess(pushstring) {
    //    alert("Success:"+pushstring);
    //alert(pushstring);
}

function OnGitterLoadFailed() {
    //alert("Failure");
}