var storyManager = function (params) {

    this._getStoryUrl = params.getStoryUrl;
    this._saveStoryUrl = params.saveStoryUrl;
    this._addNewStoryUrl = params.addNewStoryUrl;
    this._getGroups = params.getGroupUrl;
    this.selectedStory = null;
    this._validateStory = null;
    this.viewMode = false;
    $storyManager = this;

    this.clickEdit = function (storyId) {
        $storyManager.viewMode = false;
        $storyManager.getSelectedStory(storyId);        
    }

    this.clickView = function (storyId) {
        $storyManager.viewMode = true;
        $storyManager.getSelectedStory(storyId);
    }

    this.getSelectedStory = function (storyId) {
        storyId = parseInt(storyId);
        var list = $("#listView").data("kendoListView");
        var array = list.dataItems();
        $storyManager.selectedStory = array.filter(function (v) {
            return v.StoryId === storyId;
        })[0];
        $storyManager.openWindow();        
    }

    this.changeGroup =function(){
        var selgroups = $("#selGroupId").data("kendoMultiSelect");
        $("#hdnGroupId").val(selgroups._values);        
    }

    this.initButtonClick = function () {
        $("#addnewStory").bind('click', function (e) {
            $storyManager.viewMode = false;
            $storyManager.openWindow();
        });
    }
    
    this.openWindow = function () {
        $("#addnewStoryWindow").kendoWindow({
            width: "420px",
            height: "400px",
            title: "Add New Story",
            resizable: false,
            modal: true,
            visible: false,
            content: $storyManager._addNewStoryUrl,
            animation: { close: { duration: 600 } },
            activate: function (e) {
                $storyManager.initWindow();
                $storyManager.initMultiSelect();
                $storyManager.setSelectrion();
                $storyManager.updateSate($storyManager.viewMode);
                $("#dpPostedOn").kendoDatePicker();
            }
        }).data("kendoWindow");
        $("#addnewStoryWindow").data("kendoWindow").center().open();
    }

    this.setSelectrion = function () {
        if ($storyManager.selectedStory !== null) {
            $("#hdnStoryId").val($storyManager.selectedStory.StoryId);
            $("#txtTitle").val($storyManager.selectedStory.Title); 
            $("#txaDescription").val($storyManager.selectedStory.Description);            
            $("#dpPostedOn").val($storyManager.selectedStory.PostedOn);
            $("#txaContent").val($storyManager.selectedStory.Content);
            $("#hdnGroupId").val($storyManager.selectedStory.GroupIds);
        }        
    }

    this.updateSate = function (viewMode) {
        if (viewMode) {
            $("#txaDescription").attr("disabled", "disabled");
            $("#txaContent").attr("disabled", "disabled");
            $("#dpPostedOn").attr("disabled", "disabled");
            $("#txtTitle").attr("disabled", "disabled");
            $("#btnAddStory").attr("disabled", "disabled");
            $("#btnCancel").attr("disabled", "disabled");
            $("#selGroupId").attr("disabled", "disabled");
        } else {
            $("#txaDescription").removeAttr("disabled");
            $("#txaContent").removeAttr("disabled");
            $("#dpPostedOn").removeAttr("disabled");
            $("#txtTitle").removeAttr("disabled");
            $("#btnAddStory").removeAttr("disabled");
            $("#btnCancel").removeAttr("disabled");
            $("#selGroupId").removeAttr("disabled");
        }
    }

    this.formValidateStory = function () {
        if ($storyManager._validateStory === null) {
            $storyManager._validateStory = $("#storyForm").kendoValidator(
                {
                    rules: {
                        dateValidation: function (input) {
                            if (input.filter("#dpPostedOn").length && $.trim(input.val()).length) {
                                var dateRegex = /^(0?[1-9]|1[0-2])\/(0?[1-9]|1\d|2\d|3[01])\/((19|20)[0-9]{2})$/;
                                return dateRegex.test(input.val());
                            }
                            return true;
                        },
                        createFuture: function (input) {
                            if ($(input).attr('id') == 'dpPostedOn') {
                                var dateRec = Date.parse($(input).val());
                                if (isNaN(dateRec)) {
                                    return true;
                                }
                                else {
                                    var currentDate = new Date();
                                    return Date.parse(currentDate) >= dateRec;
                                }
                            }
                            else {
                                return true;
                            }
                        },
                    },
                    messages: {
                        dateValidation: "Invalid date format",
                        createFuture: "Date can't be future",
                    }
                }).data("kendoValidator");
        }
    }

    this.refreshListView = function () {
        var list = $("#listView").data("kendoListView");
        list.dataSource.read();
    }

    this.initListView = function () {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: $storyManager._getStoryUrl
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
        var window = $("#addnewStoryWindow").data("kendoWindow");
        window.close();
        $storyManager.viewMode = false;
    }

    this.initWindow = function () {
        $("#btnAddStory").bind('click', function (e) {
            $storyManager.formValidateStory();
            if ($storyManager._validateStory.validate()) {                
                $.ajax({
                    type: 'POST',
                    url: $storyManager._saveStoryUrl,
                    data: JSON.stringify({ StoryId: $("#hdnStoryId").val(), Title: $("#txtTitle").val(), Description: $("#txaDescription").val(), PostedOn: $("#dpPostedOn").val(), Content: $("#txaContent").val(), GroupIds: $("#hdnGroupId").val() }),
                    success: function (data) {
                        $storyManager.refreshListView();
                        $storyManager.selectedStory = null;
                        $storyManager.viewMode = false;
                        $storyManager.onCloseWindow();
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                    },
                    contentType: 'application/json',
                    dataType: 'json'
                });
            }
        });

        $("#btnCancel").bind('click', function (e) {
            $storyManager.onCloseWindow();
        });
    }

    this.initMultiSelect = function () {
        $("#selGroupId").kendoMultiSelect({
            placeholder: "-- select --",
            dataTextField: "Text",
            dataValueField: "Value",
            autoBind: false,
            change: $storyManager.changeGroup,
            dataSource: {
                transport: {
                    read: {
                        url: $storyManager._getGroups
                    }
                }
            }
        });
    }

    this.parseJsonDate = function (jsonDateString) {
        if (jsonDateString != null) {
            var date = new Date(parseInt(jsonDateString.replace('/Date(', '')));
            var year = date.getFullYear();
            var month = date.getMonth() + 1;
            var day = date.getDate();
            return month + "/" + day + "/" + year;

        } else {
            return "";
        }
    }

    this.init = function () {        
        $storyManager.initMultiSelect();
        $storyManager.initButtonClick();
        $storyManager.initListView();                
    }();

}