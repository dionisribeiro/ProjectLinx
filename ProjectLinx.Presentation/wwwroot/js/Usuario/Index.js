var usuarioId;
$('.excluir-usuario').click(function () {
    usuarioId = $(this).attr("usuarioId");
    $('#delete-modal-user').attr("usuarioId", usuarioId).modal();
});


$("#confirma-exclusao").on('click', function () {
    $.ajax({
        type: "DELETE",
        url: "/Usuario/Delete/?id=" + usuarioId,
        success: function () {
            location.reload();
        }
    });
});