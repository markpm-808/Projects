$(document).ready(function () {
    displayItems()
});

function displayItems() {
    refresh();
    var tally = 1;

    $.ajax({
        type: 'GET',
        url: 'http://tsg-vending.herokuapp.com/items',
        success: function (itemArray) {
            $.each(itemArray, function (index, item) {
                // display id
                var id_number = 'index' + tally;
                $("#" + id_number).append(item.id);

                // display name
                var item_name = 'name' + tally;
                if (item.name.length >= 30) {
                    var s = String(item.name);
                    s = s.substring(0, 29);
                    $("#" + item_name).append(s);
                }
                else {
                    $("#" + item_name).append(item.name);
                }

                // display price
                var item_price = 'price' + tally;
                $("#" + item_price).append("$" + item.price);

                // display quantity
                var item_quantity = 'quantity' + tally;
                $("#" + item_quantity).append("Quantity Left: " + item.quantity);

                // change vending item visibility 
                var vending_item = 'item' + tally;
                $("#" + vending_item).css('visibility', 'visible');
                tally++;
            })
        },
        error: function () {
            $('#error').append('...Error fetching Vending Machine Data');
        }
    })
}

// clear item fields
function refresh() {
    for (i = 1; i < 13; i++) {
        var id_number = 'index' + i;
        var item_name = 'name' + i;
        var item_price = 'price' + i;
        var item_quantity = 'quantity' + i;

        $("#" + id_number).empty();
        $("#" + item_name).empty();
        $("#" + item_price).empty();
        $("#" + item_quantity).empty();
    }
    $('#error').empty();
}

function addMoney(input) {
    $("#change").val('');
    var x = document.getElementById("runningTotal");
    var currentVal = +x.value + +input;
    var currentVal = currentVal.toFixed(2);
    
    x.value = currentVal;
}

function selectItem(id) {
    removeHighlight();
    addHighlight(id);
    $("#messages").val('');
    $("#change").val('');
    var element = "#" + id;
    var x = $(element).text();
    $("#itemSelect").val(x);
    selectItem.called = true;
}

function addHighlight(id) {
    var num = id.slice(5);
    var x = '#item' + num;
    $(x).addClass("highlight");
}

function removeHighlight() {
    for (i = 1; i < 13; i++) {
        var x = '#item' + i;
        $(x).removeClass("highlight");
    }
}

function makePurchase() {
    var amount = document.querySelector("#runningTotal").value;
    if (!selectItem.called) {
        var x = "Please make a selection";
        $("#messages").val(x);
    }
    else if (amount == '') {
        var msg = "Please input money";
        $("#messages").val(msg);
    }
    else {
        var id = document.querySelector("#itemSelect").value;
        $("#change").val('');
        
        $.ajax({
            type: 'POST',
            url: 'http://tsg-vending.herokuapp.com/money/' + amount + '/item/' + id,
            headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function (data) {
                var quarters = data.quarters;
                var dimes = data.dimes;
                var nickels = data.nickels;
                var pennies = data.pennies;

                updateTotal(quarters, dimes, nickels, pennies);

                displayItems();
                $("#messages").val('Thank You!!');
                
            },
            error: function(jqXHR) {
                var data = jqXHR.responseJSON;
                var output = insufficientFunds(data.message);
                $("#messages").val(output);
                
            }
        })
    }
}

function updateTotal(quarters, dimes, nickels, pennies) {
    var change = (quarters * .25) + (dimes * .10) + (nickels * .05) + (pennies * .01);
    change = change.toFixed(2);
    $("#runningTotal").val(change);
}

function changeReturn() {
    displayItems();
    var amount = document.querySelector("#runningTotal").value;
    calculateChange(amount);
    $("#itemSelect").val('');
    $("#messages").val('');
    removeHighlight();
    selectItem.called = false;
}

// convert total amount change to # of coins
function calculateChange(amount) {
    var change = amount * 100;
    var quarters = Math.floor(change / 25);
    change %= 25;
    var dimes = Math.floor(change / 10);
    change %= 10;
    var nickels = Math.floor(change / 5);
    change %= 5;
    var pennies = Math.floor(change / 1);
    getChange(quarters, dimes, nickels, pennies);
}

// diplay change returned
function getChange(quarters, dimes, nickels, pennies) {
    var x = '';
    if (quarters != 0) {
        if (quarters == 1) {
            x += quarters + ' Quarter '
        }
        else {
            x += quarters + ' Quarters '
        }
    }
    if (dimes != 0) {
        if (dimes == 1) {
            x += dimes + ' Dime '
        }
        else {
            x += dimes + ' Dimes '
        }
    }
    if (nickels != 0) {
        if (nickels == 1) {
            x += nickels + ' Nickel '
        }
        else {
            x += nickels + ' Nickels '
        }
    }
    if (pennies != 0) {
        if (pennies == 1) {
            x += pennies + ' Penny '
        }
        else {
            x += pennies + ' Pennies '
        }
    }
    $("#change").val(x);
    $("#runningTotal").val('');
}

// edit insufficient fund msg to include $
function insufficientFunds(data) {

    var strMatch = "Please deposit: ";
    var strChange = "Please deposit: $";
    var str = String(data);

    if (str.substring(0, 16) == strMatch) {
        str = str.replace(strMatch, strChange);
    }
    return str;
}