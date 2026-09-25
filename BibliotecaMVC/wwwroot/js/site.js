// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// SweetAlert - Confirmación Eliminar
// ------------------------------------------------------------------
$(function () {

    $(document).on('submit', 'form.swal-delete-form', function (e) {
        e.preventDefault();
        var form = this;

        Swal.fire({
            title: '¿Eliminar registro?',
            text: 'Esta acción no se puede deshacer.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        })
            .then(function (result) {
                if (result.isConfirmed) {
                    form.submit();
                }
            });
    });


    // SweetAlert - Confirmación Guardar
    // -----------------------------------------------------------
    $(document).on('submit', 'form.swal-save-form', function (e) {
        e.preventDefault();
        var form = this;

        Swal.fire({
            title: '¿Guardar cambios?',
            text: 'Confirma que deseas guardar la información.',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sí, guardar',
            cancelButtonText: 'Cancelar'
        })
            .then(function (result) {
                if (result.isConfirmed) {
                    form.submit();
                }
            });
    });

 
    // SweetAlert - Mensaje de Éxito (Genérico)
    // --------------------------------------------------------
    var successMessage = $('#success-message').data('message');

    if (successMessage) {
        Swal.fire({
            icon: 'success',
            title: 'Operación exitosa',
            text: successMessage,
            timer: 2000,
            showConfirmButton: false
        });
    }

});
