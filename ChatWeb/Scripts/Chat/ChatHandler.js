$(function () {
    var chat = $.connection.chatHub;
    chat.client.addMessage = function (name, message) {
        addChat(chat, chat.sendUserName);
    };
    
    chat.client.onConnected = function (id, userName, allUsers) {
        $('#hdId').val(id);
        $('#username').val(userName);
        $('#currentUser').html(userName);

        for (i = 0; i < allUsers.length; i++) {            
            addUser(chat, allUsers[i].ConnectionId, allUsers[i].Name);
        }
    }

    chat.client.onNewUserConnected = function (id, name) {
        addUser(chat, id, name);
    }

    chat.client.onUserDisconnected = function (id, userName) {
        removeUser(id);
    }

    $.connection.hub.start().done(function () {
        chat.server.getUserName().then((user) => {
            chat.userName = user; $("#txtUserName").val(chat.userName)
            chat.server.connect(chat.userName);
        });

        $('#sendmessage').click(function () {
            sendMessage();
        });
        $('#message').on('keypress', function (e) {
            if (e.which === 13) {
                sendMessage();
            }
        });
    });

    function sendMessage() {
        chat.server.send(chat.userName, chat.sendUserName, $('#message').val());
        $('#message').val('');
    }

    function addUser(chat, id, name) {   
        let userDiv = $("#user_Content");
        if (name === chat.userName)
            return;
        userDiv.append(getChatUserHTML(id, name));
        let children = userDiv.children();
        if (!children || children.length === 0)
            return;
        $(children[children.length - 1]).click(function (sender, e) {
            let sendUserName = this.getElementsByClassName('user_Name')[0].innerText;
            chat.user = sender.currentTarget;
            addChat(chat, sendUserName);
        });
    }

    function addChat(chat, sendUserName) {
        let beginDate = $('#dateFrom').val();
        let endDate = $('#dateTo').val();
        chat.sendUserName = sendUserName;
        chat.server.getChats(chat.userName, chat.sendUserName, beginDate, endDate).then((messages) => {
            let msgHistory = $('.msg_history');
            msgHistory.children().remove();
            for (let i = 0; i < messages.length; i++) {
                let message = messages[i];
                if (message.From === chat.sendUserName)
                    msgHistory.append(getIncomeMessage(message.Message, message.CreateDate));
                else
                    msgHistory.append(getOutgoinMessage(message.Message, message.CreateDate));
            }
        }); 
        setSelected();
    }

    function removeUser(id) {
        $("#user_Content").children('#' + id).remove();
        console.log($("#user_Content").children())
    }

    $(document).ready(function() {
        let begin = dtPickerInitialize('dateFrom', getBeginOfMonth());
        let end = dtPickerInitialize('dateTo', getEndOfMonth());
        begin.on("dp.change", function () { addChat(chat, chat.sendUserName); });
        end.on("dp.change", function () { addChat(chat, chat.sendUserName); });
    });

    function dtPickerInitialize(idValue, defaultDate) {
        let dtPicker =
        $('#'+idValue).datetimepicker({
            format: "DD.MM.YYYY",
            defaultDate: defaultDate
        }).datetimepicker();
        return dtPicker;
    };

    function getBeginOfMonth() {
        var date = new Date();
        return new Date(date.getFullYear(), date.getMonth(), 1);
    
    }

    function getEndOfMonth() {
        var date = new Date();
        return new Date(date.getFullYear(), date.getMonth() + 1, 0);
    }

    function setSelected() {
        let chatUsers = $('.chat_list');
        for (let i = 0; i < chatUsers.length; i++) {
            let current = $(chatUsers[i]);
            if (current.attr('id') === $(chat.user).attr('id'))
                current.attr('class', 'chat_list active_chat');
            else
                current.attr('class', 'chat_list');
        }
    }

    $('#deleteTest').click(function () {
        console.log('delete');
        let msgHistory = $('.msg_history');
        msgHistory.children().remove();
    });
});