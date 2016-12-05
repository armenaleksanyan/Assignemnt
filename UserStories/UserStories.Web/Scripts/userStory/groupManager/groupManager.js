var groupManager = function (params) {
    this._getGroupUrl = params.getGroupUrl;
    this._saveGroupUrl = params.saveGroupUrl;
    this._addNewGroupUrl = params.addNewGroupUrl;
    this._validateGroup = null;
    $groupManager = this;

    this.initButtonClick = function () {
        $("#addnewGroup").bind('click', function (e) {
            $("#addnewGroupWindow").kendoWindow({
                width: "400px",
                height: "300px",
                title: "Add New Group",
                resizable: false,
                modal: true,
                visible: false,
                content: $groupManager._addNewGroupUrl,
                animation: { close: { duration: 600 } },
                activate: function (e) {
                    $groupManager.initWindow();
                },
                //close: $groupManager.onCloseWindow
            }).data("kendoWindow");
            $("#addnewGroupWindow").data("kendoWindow").center().open();
        });        
    }

    this.formValidateGroup = function () {
        if ($groupManager._validateGroup === null) {
            $groupManager._validateGroup = $("#groupForm").kendoValidator(
                {
                    rules: { },
                    messages: { }
                }).data("kendoValidator");
        }

    }

    this.refreshListView=function(){
        var list = $("#listView").data("kendoListView");
        list.dataSource.read();
    }

    this.initListView = function () {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: $groupManager._getGroupUrl
                }
            },
            pageSize: 8
        });
        $("#pager").kendoPager({
            dataSource: dataSource
        });
        $("#listView").kendoListView({
            dataSource: dataSource,
            template: kendo.template($("#template").html())
        });
    }

    this.onCloseWindow = function () {
        var window = $("#addnewGroupWindow").data("kendoWindow");
        window.close();
    }

    this.initWindow = function () {
        $("#btnAdd").bind('click', function (e) {
            $groupManager.formValidateGroup();
            if ($groupManager._validateGroup.validate()) {               
                $.ajax({
                    type: 'POST',
                    url: $groupManager._saveGroupUrl,
                    data: JSON.stringify({ Name: $("#txtName").val(), Description: $("#txaDescription").val() }),
                    success: function (data) {
                        $groupManager.refreshListView();
                        $groupManager.onCloseWindow();
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                    },
                    contentType: 'application/json',
                    dataType: 'json'
                });
            }
        });

        $("#btnCancel").bind('click', function (e) {
            $groupManager.onCloseWindow();
        });
    }

    this.init = function () {
        $groupManager.initButtonClick();
        $groupManager.initListView();
        $groupManager.initWindow();
    }();
};