$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);

        const swal = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-danger mx-2',
                cancelButton: 'btn btn-light'
            },
            buttonsStyling: false
        });

        swal.fire({
            title: 'Are you sure that you need to delete this employee?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Employee/Delete/${btn.data('id')}`,  // Ensure the URL matches your routing
                    method: 'DELETE',
                    success: function () {
                        swal.fire(
                            'Deleted!',
                            'Employee has been deleted.',
                            'success'
                        );
                        btn.parents('tr').fadeOut();
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText); // Log error for debugging
                        swal.fire(
                            'Oooops...',
                            'Something went wrong. ' + error,
                            'error'
                        );
                    }
                });
            }
        });
    });
});
