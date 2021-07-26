$(document).ready(function () {

});
//openWeather key:8177cd1f9b79842c79063b7e72029f19

function getWeather() {

    refresh();

    var zipData = getZipcode();
    var unitData = getUnits();
    var key = "8177cd1f9b79842c79063b7e72029f19";
    var tempSymbol = '';
    var windSymbol = '';

    if (unitData == 'Imperial') {
        tempSymbol = ' F';
        windSymbol = ' miles/hour'
    }
    else {
        tempSymbol = ' C';
        windSymbol = ' knots'
    }

    currentConditions(zipData, unitData, key, tempSymbol, windSymbol);
    fiveDayForecast(zipData, unitData, key, tempSymbol);

}



function currentConditions(zipData, unitData, key, tempSymbol, windSymbol) {

    var zipData = zipData;
    var unitData = unitData;
    var key = key;
    var tempsymbol = tempSymbol;
    var windSymbol = windSymbol;

    $.ajax({
        type: 'GET',
        url: 'http://api.openweathermap.org/data/2.5/weather?zip=' + zipData + ',us&appid=' + key + '&units=' + unitData,
        success: function (data) {

            //display city name
            var cityName = data.name;
            $("#cityNameSpan").append(cityName);


            //get icon name
            var iconName = data.weather[0].icon;
            var urlIcon = 'http://openweathermap.org/img/w/' + iconName + '.png';
            //add url to img src
            $("#icon").attr('src', urlIcon);

            //get descrpiton
            var desc = data.weather[0].description;
            $("#descrpitonSpan").append(desc);

            //display temperature
            var tempData = data.main.temp;
            $("#temp").append(tempData);
            $("#temp").append(tempSymbol);


            //display humidity
            var humidityData = data.main.humidity;
            $("#humidity").append(humidityData + '%');

            //display wind speed
            var windData = data.wind.speed;
            $("#wind").append(windData + windSymbol);
            
            $("#currentConditionsDiv").show();
        },
        error: function () {
            $('#errorMessages').empty();
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Zip code: please enter a 5-digit zip code.'));
            $("#currentConditionsDiv").hide();
        }
    })
}


function fiveDayForecast(zipData, unitData, key, tempSymbol) {

    
    var zipData = zipData;
    var unitData = unitData;
    var key = key;
    var tempsymbol = tempSymbol;


    $.ajax({
        type: 'GET',
        url: 'http://api.openweathermap.org/data/2.5/forecast?zip=' + zipData + ',us&appid=' + key + '&units=' + unitData,
        success: function (data) {
            //get icon name
            var iconNameOne = data.list[0].weather[0].icon;
            var urlIconOne = 'http://openweathermap.org/img/w/' + iconNameOne + '.png';
            
            var iconNameTwo = data.list[1].weather[0].icon;
            var urlIconTwo = 'http://openweathermap.org/img/w/' + iconNameTwo + '.png';

            var iconNameThree = data.list[2].weather[0].icon;
            var urlIconThree = 'http://openweathermap.org/img/w/' + iconNameThree + '.png';

            var iconNameFour = data.list[3].weather[0].icon;
            var urlIconFour = 'http://openweathermap.org/img/w/' + iconNameFour + '.png';

            var iconNameFive = data.list[4].weather[0].icon;
            var urlIconFive = 'http://openweathermap.org/img/w/' + iconNameFive + '.png';

            //add url to img src
            $("#dayOneIcon").attr('src', urlIconOne);
            $("#dayTwoIcon").attr('src', urlIconTwo);
            $("#dayThreeIcon").attr('src', urlIconThree);
            $("#dayFourIcon").attr('src', urlIconFour);
            $("#dayFiveIcon").attr('src', urlIconFive);


            //get descrpiton
            var descOne = data.list[0].weather[0].description;
            $("#dayOneDesc").append(descOne);

            var descTwo = data.list[1].weather[0].description;
            $("#dayTwoDesc").append(descTwo);

            var descThree = data.list[2].weather[0].description;
            $("#dayThreeDesc").append(descThree);

            var descFour = data.list[3].weather[0].description;
            $("#dayFourDesc").append(descFour);

            var descFive = data.list[4].weather[0].description;
            $("#dayFiveDesc").append(descFive);

            //get high/low
            var dayOneHigh = data.list[0].main.temp_max;
            var dayOneLow = data.list[0].main.temp_min;
            $("#dayOneHigh").append('H ' + dayOneHigh + tempsymbol);
            $("#dayOneLow").append('L ' + dayOneLow + tempsymbol);

            var dayTwoHigh = data.list[1].main.temp_max;
            var dayTwoLow = data.list[1].main.temp_min;
            $("#dayTwoHigh").append('H ' + dayTwoHigh + tempsymbol);
            $("#dayTwoLow").append('L ' + dayTwoLow + tempsymbol);

            var dayThreeHigh = data.list[2].main.temp_max;
            var dayThreeLow = data.list[2].main.temp_min;
            $("#dayThreeHigh").append('H ' + dayThreeHigh + tempsymbol);
            $("#dayThreeLow").append('L ' + dayThreeLow + tempsymbol);

            var dayFourHigh = data.list[3].main.temp_max;
            var dayFourLow = data.list[3].main.temp_min;
            $("#dayFourHigh").append('H ' + dayFourHigh + tempsymbol);
            $("#dayFourLow").append('L ' + dayFourLow + tempsymbol);

            var dayFiveHigh = data.list[4].main.temp_max;
            var dayFiveLow = data.list[4].main.temp_min;
            $("#dayFiveHigh").append('H ' + dayFiveHigh + tempsymbol);
            $("#dayFiveLow").append('L ' + dayFiveLow + tempsymbol);








            $("#fiveDayForecastDiv").show();
        },
        error: function () {

            $('#errorMessages').empty();
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Zip code: please enter a 5-digit zip code.'));
            $("#fiveDayForecastDiv").hide();

        }

    })
    
}










//clear old current condition data
function refresh() {
    $('#errorMessages').empty();
    $("#cityNameSpan").empty();
    $("#descrpitonSpan").empty();
    $("#temp").empty();
    $("#humidity").empty();
    $("#wind").empty();

    //five day forecast
    $("#dayOneDesc").empty();
    $("#dayTwoDesc").empty();
    $("#dayThreeDesc").empty();
    $("#dayFourDesc").empty();
    $("#dayFiveDesc").empty();

    $("#dayOneHigh").empty();
    $("#dayOneLow").empty();
    $("#dayTwoHigh").empty();
    $("#dayTwoLow").empty();
    $("#dayThreeHigh").empty();
    $("#dayThreeLow").empty();
    $("#dayFourHigh").empty();
    $("#dayFourLow").empty();
    $("#dayFiveHigh").empty();
    $("#dayFiveLow").empty();
}




//returns the zipcode inputted
function getZipcode() {

    var zip = document.getElementById("inputZipcode").value;
    return zip;
}




//returns either imperial or metric
function getUnits() {
    var e = document.getElementById("selectUnits");
    var unit = e.options[e.selectedIndex].text;
    return unit;
}

