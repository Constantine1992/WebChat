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
        $(children[children.length - 1]).click(function () {
            let sendUserName = this.getElementsByClassName('user_Name')[0].innerText;
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
    }

    function removeUser(id) {
        $("#user_Content").children('#' + id).remove();
        console.log($("#user_Content").children())
    }

    function getChatUserHTML(id, name) {
    let chatUserHTML = '<div class="chat_list" id="' +id+'">'+
        '<div class="chat_people" >'+
            '<div class="chat_img"> <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil"> </div>'+
               ' <div class="chat_ib">'+
                    '<h5 class="user_Name">'+name+' <span class="chat_date"></span></h5>'+
                    '<p>'+

                    '</p>'+
                '</div>'+
            '</div>'+
            '</div >';
        return chatUserHTML;
    }

    function getIncomeMessage(message, date) {
    let incomeMessage = '<div class="incoming_msg">' +
        '<div class="incoming_msg_img"> <img src="https://ptetutorials.com/images/user-profile.png" alt="sunil"> </div>'+
            '<div class="received_msg">'+
                '<div class="received_withd_msg">'+
                    '<p>'+
                        message +'</p>'+
                    '<span class="time_date"> '+date+''+
                '</div>'+
            '</div>'+
            '</div>';
        return incomeMessage;
    }

    function getOutgoinMessage(message, date) {
        var outgoinMessage = '<div class="outgoing_msg">' +
            '<div class="sent_msg">' +
            '<p>  ' + message + ' </p>' +
            '<span class="time_date"> '+date+'</span>' +
            '</div>'+
        '</div>';
        return outgoinMessage;
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

});