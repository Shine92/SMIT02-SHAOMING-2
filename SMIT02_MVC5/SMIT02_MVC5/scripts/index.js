/*會員登入視窗*/
$(document).ready(function () {
    $("#Member").click(function () {
        $("#myModal").modal();
    });

    $("#SignUpBtn").click(function () {
        window.location.replace("/Member/Register");
    });
});

$(document).ready(function () {
    $("LoginBtn").click(function () {
        window.location.replace("/Member/Login");
    });
});

$(document).ready(function () {
    $("loginItem").click(function () {
        window.location.replace("/Member/Login");
    });
});
/*會員登入視窗 END*/

/*系統管理員*/
/*刪除會員*/
$(document).ready(function () {
    $(".memberDelete").click(function () {
        /*取得最接近Row的Id*/
        var trid = $(this).closest('tr').attr('id');
        $.ajax({
            type: "delete",
            url: "/odata/Members(" + trid + ")"
        }).then(function () {
            alert("刪除會員成功");
            window.location.replace("/Manage/MemberBoard");
        })
    });
});
/*刪除會員 END*/
/*編輯會員*/
$(document).ready(function () {
    $(".memberEditBtn").click(function () {
        var trid = $(this).closest('tr').attr('id');
        $.ajax({
            type: "get",
            url: "/odata/Members(" + trid + ")",
            dataType: "json",
            success: function (data) {

                var objJson = JSON.stringify(data);
                var obj = JSON.parse(objJson);
                var selectIndex = ((obj.IsAdmin == true) ? 1 : 0);
                $("#editModal").modal();
                $("#editId").val(obj.Id);
                $("#accountTextBox").val(obj.Account);
                $("#nameTextBox").val(obj.Name);
                $("#passwordTextBox").val(obj.Password);
                $("#emailTextBox").val(obj.Email);
                /*radio-idea.blogspot.tw/2014/05/jqueryselect.html*/
                $("#isAdmin")[0].selectedIndex = selectIndex;
            },
            error: function () { }
        })
    })
    /*編輯會員 END*/
    /*上傳會員資料*/
    $("#okButton").click(function () {
        var editIndex = $("#editId").val();
        var IsAdmin = ($("#isAdmin").val() == 1) ? true : false;
        var updatedItem = {
            Id: editIndex,
            Account: $("#accountTextBox").val(),
            Password: $("#passwordTextBox").val(),
            Name: $("#nameTextBox").val(),
            Email: $("#emailTextBox").val(),
            IsAdmin: IsAdmin
        }

        $.ajax({
            type: "put",
            url: "/odata/Members(" + editIndex + ")",
            data: JSON.stringify(updatedItem),
            contentType: "application/json",
            success: function () {
                alert("會員修改成功");
                window.location.replace("/Manage/MemberBoard");
            }
        })
    })
})

/*上傳會員資料 END*/

/*編輯商品*/

$(document).ready(function () {
    $(".editItem").click(function () {
        var iIndex = $(this).closest("li").attr('id');
        $.ajax({
            type: "get",
            url: "/odata/Items(" + iIndex + ")",
            dataType: "json",
            success: function (data) {
                var objJson = JSON.stringify(data);
                var obj = JSON.parse(objJson);
                $("#editItemModal").modal();
                $("#editId").val(obj.Id);
                $("#editImage").val(obj.Image);
                $("#productNameTextBox").val(obj.Name);
                $("#productPriceTextBox").val(obj.Price);
                $("#productUnitTextBox").val(obj.UnitStock);

            }
        })
    })

    $("#editItemOkButton").click(function () {
        var editItemIndex = $("#editId").val();
        var updatedProductItem = {
            Id: editItemIndex,
            Name: $("#productNameTextBox").val(),
            Price: $("#productPriceTextBox").val(),
            Image: $("#editImage").val(),
            UnitStock: $("#productUnitTextBox").val()
        }

        //alert("OK");
        $.ajax({
            type: "put",
            url: "/odata/Items(" + editItemIndex + ")",
            data: JSON.stringify(updatedProductItem),
            contentType: "application/json",
            success: function () {
                alert("商品編輯成功");
                window.location.replace("/Manage/ItemList");
            }
        })
    })
})
/*編輯商品END*/
/*刪除商品*/
$(document).ready(function () {
    $(".deleteItem").click(function () {
        var iIndex = $(this).closest("li").attr('id');
        $.ajax({
            type: "delete",
            url: "/odata/Items(" + iIndex + ")",
            success: function () {
                alert("商品刪除成功");
                window.location.replace("/Manage/ItemList");
            }
        })

    })
})
/*刪除商品 END*/
/*系統管理員 END*/
/*一般使用者*/
/*加入購物車*/
$(document).ready(function () {
    $("#addShoppingCart").click(function () {
        var carId = $("#memberId").val();
        var itemId = $("#itemDteailId").val();
        var itemUnit = $("#sel1").val();

        var newCartItem = {
            Cart_Id: carId,
            Item_Id : itemId,
            Unit: itemUnit,
            Member_Id: carId,
        }

        //alert("carId:" + carId + "itemId:" + itemId + "itemUnit" + itemUnit);
        $.ajax({
            type: "post",
            url: "/odata/CartBuys",
            data: JSON.stringify(newCartItem),
            contentType: "application/json",
            success: function () {
                alert("加入購物車成功");
            },
            error:function(){
                alert("加入購物車成功");
            }
        })
        
        function GetItemCount() {
            $.ajax({
                type: "get",
                url: "/odata/CartBuys",
                dataType: "json",
                success: function (data) {
                    var objJson = JSON.stringify(data);
                    alert(objJson);
                },
                error: function () {
                    alert("Json error");
                }

            })
        }

    })
    /*加入購物車 END*/
    
})
/*一般使用者 END*/