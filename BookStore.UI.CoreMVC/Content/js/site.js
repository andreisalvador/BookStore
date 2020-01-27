var webApiUrl = '/bookstore';

$(document).ready(function () {
    // Activate tooltip
    $('[data-toggle="tooltip"]').tooltip();

    $.ajax({
        type: "GET",
        url: webApiUrl,
        success: function (books) {
            for (let index = 0; index < books.length; index++) {

                var dataId = 'data-id=' + books[index].id;

                tr = $('<tr/>');
                tr.append('<td>' + books[index].title + '</td>');
                tr.append('<td>' + books[index].authorName + '</td>');
                tr.append('<td>' + new Date(books[index].releaseDate) + '</td>');
                tr.append('<td> R$ ' + books[index].price + '</td>');
                tr.append('<td><a href="#editBookModal" class="edit" ' + dataId + ' data-toggle="modal"><i class="material-icons" data-toggle="tooltip" title="Edit">&#xE254;</i></a><a href="#deleteBookModal" class="delete" ' + dataId + ' data-toggle="modal"><i class="material-icons" data-toggle="tooltip" title="Delete">&#xE872;</i></a></td>');

                $('#table-body-books').append(tr);
            }
        }
    });

    AddActionOnShowModal($('#editBookModal'), FillModal);
    AddActionOnShowModal($('#deleteBookModal'), (e, form) => {
        var id = $(e.relatedTarget).data('id');
        form.find('input[id="record-id"]').val(id);
    });

    Delete($('#btn-modal-del'), $('#deleteBookModal'));
    AddOrUpdate($('#btn-modal-add'), $('#addBookModal'), PostRequest);
    AddOrUpdate($('#btn-modal-edt'), $('#editBookModal'), PutRequest);

});

function FillModal(e, form) {
    var id = $(e.relatedTarget).data('id');

    form.find('input[id="record-id"]').val(id);

    GetRequest(id).success((book) => SetBookValues(form, book));
}

function AddActionOnShowModal(form, action) {
    form.on('shown.bs.modal', (e) => {
        action(e, form);
    });
}

function Delete(button, form) {
    button.on('click', (e) => {
        var id = form.find('input[id="record-id"]').val();

        console.log(id);
        DeleteRequest(id);
        form.submit();
    });
}

function AddOrUpdate(button, form, requestFunction) {
    button.on('click', (e) => {

        var id = $(e.relatedTarget).data('id');

        requestFunction(id, GetBookValues(form));

        e.preventDefault();
    });
}


function Request(requestType, book, id = 0) {

    console.log("Doing request of type " + requestType);

    return $.ajax({
        'url': id > 0 ? webApiUrl + id : webApiUrl,
        'type': requestType,
        'data': JSON.stringify(book),
        'contentType': 'application/json',
        error: (error) => console.log(error)
    })
}

function GetRequest(id = 0) {
    return this.Request('GET', {}, id);
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
    form.find('input[name="ReleaseDate"]').val(new Date(book.releaseDate).toLocaleDateString());
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
