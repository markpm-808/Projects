$(document).ready(function () {
    loadDvds()
});

function loadDvds() {
    $(location).attr('href', 'http://localhost:50361/home.html?#')
    clearDvdTable();
    var contentRows = $("#contentRows");

    $.ajax({
        type: 'GET',
        url: 'http://localhost:50361/dvds',
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.title;
                var releaseDate = dvd.releaseYear;
                var director = dvd.director;
                var rating = dvd.rating;
                var dvdId = dvd.id;
                
                var row = '<tr>';
                row += '<td><a href="#" onClick= "dvdInfo(' + dvdId + ');">' + title + '</a></td>';
                row += '<td>' + releaseDate + '</td>';
                row += '<td>' + director + '</td>';
                row += '<td>' + rating + '</td>';
                row += '<td><a href="#" onClick= "edit(' + dvdId + ');"> <u>Edit</u></a> | <a href="#" onClick= "deleteDvd(' + dvdId + ');"> <u>Delete</u></a></td>';
                row += '</tr>';

                contentRows.append(row);
            })
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }
    })
}

function search() {
    var params;
    clearDvdTable();
    var contentRows = $("#contentRows");
    params = 'SearchCategory=' + $('#searchCategory option:selected').attr('value') + '&' + 'SearchTerm=' + $('#searchTerm').val();
    $.ajax({
        type: 'GET',
        url: 'http://localhost:50361/api/dvds/search?' + params,
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.title;
                var releaseDate = dvd.releaseYear;
                var director = dvd.director;
                var rating = dvd.rating;
                var dvdId = dvd.id;
                
                var row = '<tr>';
                row += '<td><a href="#" onClick= "dvdInfo(' + dvdId + ');">' + title + '</a></td>';
                row += '<td>' + releaseDate + '</td>';
                row += '<td>' + director + '</td>';
                row += '<td>' + rating + '</td>';
                row += '<td><a href="#" onClick= "edit(' + dvdId + ');"> <u>Edit</u></a> | <a href="#" onClick= "deleteDvd(' + dvdId + ');"> <u>Delete</u></a></td>';
                row += '</tr>';

                contentRows.append(row);
            })
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('No results for your search. Please try again later.'));
        }
    })
}

function dvdInfo(dvdId) {
    var dvdInfoDiv = $("#dvdInfo");
    clearDetails();
    
    $.ajax({
        type: 'GET',
        url: 'http://localhost:50361/dvd/' + dvdId,
        success: function (data) {
            
            var title = data.title
            var releaseDate = data.releaseYear;
            var director = data.director;
            var rating = data.rating;
            var notes = data.notes;

            var row = '<h1>' + title + '</h1>';
            row += '<hr style="height:.75px;background-color:black">'
            row += '<p>Release Year:' + '<span style="padding-left: 58px">' + releaseDate + '</span></p>';
            row += '<p>Director:' + '<span style="padding-left: 87px">' + director + '</span></p>';
            row += '<p>Rating:' + '<span style="padding-left: 99px">' + rating + '</span></p>';
            row += '<p>Notes:' + '<span style="padding-left: 100px">' + notes + '</span></p>';

            dvdInfoDiv.append(row);
       
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }
    })
    $("#library").hide();
    $("#detailsPage").show();
}

function addDvd() {
    
    $.ajax({
        type: 'POST',
        url: 'http://localhost:50361/dvd',
        data: JSON.stringify({
            title: $('#addDvdTitle').val(),
            releaseYear: $('#addDvdYear').val(),
            director: $("#addDirector").val(),
            rating: $('#addRating').val(),
            notes: $('#addDvdNotes').val()

        }),
        headers: {
            'Accept': 'application/json',
            'Content-type': 'application/json'
        },
        'dataType': 'json',
        success: function () {
            $('#errorMessages').empty();
            $('#addDvdTitle').val('');
            $('#addDvdYear').val('');
            $('#addDirector').val('');
            $('#addRating').prop('selectedIndex', 0);
            $('#addDvdNotes').val('');
            loadDvds();
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }

    })
    hideAddForm();
}

function editDvd(dvdId) {
    
    $.ajax({
        type: 'PUT',
        url: 'http://localhost:50361/dvd/' + dvdId,
        data: JSON.stringify({
            id: dvdId,
            title: $('#editDvdTitle').val(),
            releaseYear: $('#editDvdYear').val(),
            director: $("#editDirector").val(),
            rating: $('#editRating').val(),
            notes: $('#editDvdNotes').val()
        }),
        dataType: 'json',
        contentType: 'application/json',
        success: function () {
            $('#errorMessages').empty();
            $('#editDvdTitle').val('');
            $('#editDvdYear').val('');
            $('#editDirector').val('');
            $('#editRating').prop('selectedIndex', 0);
            $('#editDvdNotes').val('');
            loadDvds();
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }

    })
    hideEditForm();
}

function create() {
    $("#library").hide();
    $('#errorMessagesAdd').empty();
    $("#addForm").show();
}

function hideAddForm() {
    $('#addForm').hide();
    $('#library').show();
}

function hideEditForm() {
    $('#editForm').hide();
    $('#library').show();
}

function hideDetails() {
    $('#detailsPage').hide();
    $('#library').show();
}

function clearDvdTable() {
    $("#contentRows").empty();
}

function clearDetails() {
    $('#dvdInfo').empty();
}

function edit(dvdId) {
    $("#library").hide();
    $("#editForm").show();

    var idInput = $("#dvdId");
    var titleInput = $("#editDvdTitle");
    var yearInput = $("#editDvdYear");
    var directorInput = $("#editDirector");
    var ratingInput = $("#editRating");
    var notesInput = $("#editDvdNotes");
   
    $.ajax({
        type: 'GET',
        url: 'http://localhost:50361/dvd/' + dvdId,
        success: function (data) {

            var id = data.id;
            var title = data.title
            var releaseDate = data.releaseYear;
            var director = data.director;
            var rating = data.rating;
            var notes = data.notes;

            idInput.val(id);
            titleInput.val(title);
            yearInput.val(releaseDate);
            directorInput.val(director);
            ratingInput.val(rating);
            notesInput.val(notes);

        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Please try again later.'));
        }
    })
}

function deleteDvd(dvdId) {
    var check = confirm("Are you sure you want to delete this DVD?")
    if (check == true) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:50361/dvd/' + dvdId,
            success: function () {
                loadDvds();
            }
        });
    }
}

function validateAdd() {
    $('#errorMessagesAdd').empty();
    var title = document.getElementById("addDvdTitle");
    var year = document.getElementById("addDvdYear");

    if (!title.checkValidity()) {
        $('#errorMessagesAdd')
            .append($('<li>')
                .attr({ class: 'list-group-item list-group-item-danger' })
                .text('Please enter a title for the Dvd'));
    }
    else if (!year.checkValidity())
    {
        $('#errorMessagesAdd')
            .append($('<li>')
                .attr({ class: 'list-group-item list-group-item-danger' })
                .text('Please enter a 4-digit year'));
    }
    else {
        addDvd();
    }
}

function validateEdit(dvdId) {
    $('#errorMessagesEdit').empty();
    var title = document.getElementById("editDvdTitle");
    var year = document.getElementById("editDvdYear");
    if (!title.checkValidity()) {
        $('#errorMessagesEdit')
            .append($('<li>')
                .attr({ class: 'list-group-item list-group-item-danger' })
                .text('Please enter a title for the Dvd'));
    }
    else if (!year.checkValidity())
    {
        $('#errorMessagesEdit')
            .append($('<li>')
                .attr({ class: 'list-group-item list-group-item-danger' })
                .text('Please enter a 4-digit year'));
    }
    else {
        editDvd(dvdId);
    }
}

function validateSearch() {
    $('#errorMessages').empty();
    var category = document.getElementById("searchCategory");
    var term = document.getElementById("searchTerm");
    if (!category.checkValidity() || !term.checkValidity()) {
        $('#errorMessages')
            .append($('<li>')
                .attr({ class: 'list-group-item list-group-item-danger' })
                .text('Both Search Category and Search term are required'));
    }
    else {
        search();
    }
}