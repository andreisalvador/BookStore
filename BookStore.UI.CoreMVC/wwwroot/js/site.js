var webApiUrl = '/api/bookstore/';
var currentEntity = 0;
$(document).ready(function () {
    // Activate tooltip
    $('[data-toggle="tooltip"]').tooltip();

    moment.locale('pt-BR');

    GetBooks();

    AddActionOnShowModal($('#editBookModal'), FillModal);
    AddActionOnShowModal($('#deleteBookModal'), (e, form) => {
        var id = $(e.relatedTarget).data('id');
        form.find('input[id="record-id"]').val(id);
    });

    Delete($('#btn-modal-del'), $('#deleteBookModal'));
    AddOrUpdate($('#btn-modal-add'), $('#addBookModal'), PostRequest);
    AddOrUpdate($('#btn-modal-edt'), $('#editBookModal'), PutRequest);

});

function GetBooks() {
    $('#table-body-books').empty();
    $.ajax({
        type: "GET",
        url: webApiUrl,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (books) {
            for (let index = 0; index < books.length; index++) {

                var dataId = 'data-id=' + books[index].id;
                tr = $('<tr/>');
                tr.append('<td>' + books[index].title + '</td>');
                tr.append('<td>' + books[index].authorName + '</td>');
                tr.append('<td>' + moment(books[index].releaseDate, "YYYY-MM-DD").format('DD/MM/YYYY') + '</td>');
                tr.append('<td> R$ ' + books[index].price + '</td>');
                tr.append('<td><a href="#editBookModal" class="edit" ' + dataId + ' onclick="SetCurrentEntity(this)" data-toggle="modal"><i class="material-icons" data-toggle="tooltip" title="Edit">&#xE254;</i></a><a href="#deleteBookModal" class="delete" ' + dataId + ' onclick="SetCurrentEntity(this)" data-toggle="modal"><i class="material-icons" data-toggle="tooltip" title="Delete">&#xE872;</i></a></td>');

                $('#table-body-books').append(tr);
            }
        }
    });
}

function SetCurrentEntity(e) {
    currentEntity = $(e).data('id');
    console.log(currentEntity, $(e).data('id'));
}

function FillModal(e, form) {
    var id = $(e.relatedTarget).data('id');

    form.find('input[id="record-id"]').val(id);

    $.ajax({
        type: "GET",
        url: webApiUrl + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (book) {
            SetBookValues(form, book);
        }
    });
}

function AddActionOnShowModal(form, action) {
    form.on('shown.bs.modal', (e) => {
        action(e, form);
    });
}

function Delete(button, form) {
    button.on('click', (e) => {
        DeleteRequest(currentEntity);
    });
}

function AddOrUpdate(button, form, requestFunction) {
    button.on('click', (e) => {
        requestFunction(currentEntity, GetBookValues(form));
        e.preventDefault();
    });
}


function Request(requestType, book, id = 0) {
    return $.ajax({
        'url': id > 0 ? webApiUrl + id : webApiUrl,
        'type': requestType,
        'data': JSON.stringify(book),
        'contentType': 'application/json',
        error: (error) => console.log(error),

        success: function (book) {
            GetBooks();
            $('.modal').modal('hide');
            console.log('success post');
        }
    });
}

function PutRequest(id, book) {
    return this.Request('PUT', book, id);
}

function DeleteRequest(id) {
    return this.Request('DELETE', {}, id);
}

function PostRequest(id = 0, book) {
    return this.Request('POST', book);
}

function SetBookValues(form, book) {
    form.find('input[name="Title"]').val(book.title);
    form.find('input[name="AuthorName"]').val(book.authorName);
    form.find('input[name="ReleaseDate"]').val(moment(book.releaseDate).format('YYYY-MM-DD'));
    form.find('input[name="Price"]').val(Number(book.price));
}

function GetBookValues(form) {
    return {
        "title": form.find('input[name="Title"]').val(),
        "authorName": form.find('input[name="AuthorName"]').val(),
        "releaseDate": form.find('input[name="ReleaseDate"]').val(),
        "price": Number(form.find('input[name="Price"]').val())
    };
}
