$(function () {
    var modal = $('#infoModal');

    modal.on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var id = button.data('bs-id');
        var email = button.data('bs-email');
        var password = button.data('bs-password');
        var name = button.data('bs-name');
        var country = button.data('bs-country');
        var city = button.data('bs-city');
        var address = button.data('bs-address');
        var phone = button.data('bs-phone');

        modal.find('#infoId').text(id);
        modal.find('#infoEmail').text(email);
        modal.find('#infoPassword').text(password);
        modal.find('#infoName').text(name);
        modal.find('#infoCountry').text(country);
        modal.find('#infoCity').text(city);
        modal.find('#infoAddress').text(address);
        modal.find('#infoPhone').text(phone);
    });
});