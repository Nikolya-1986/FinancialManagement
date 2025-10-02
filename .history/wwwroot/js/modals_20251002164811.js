document.addEventListener('DOMContentLoaded', function() {
    function openRolesModal(userId, currentRole) {
        var userIdSpan = document.getElementById('modalUserIdDisplay');
        var userIdInput = document.getElementById('modalUserId');
        var select = document.getElementById('roleSelect');
        var hiddenRole = document.getElementById('selectedRole');

        if (userIdSpan && userIdInput && select && hiddenRole) {
            userIdSpan.textContent = userId;
            userIdInput.value = userId;
            select.value = currentRole;
            hiddenRole.value = currentRole;

            var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
            myModal.show();
        } else {
            console.error('Один или несколько элементов не найдены');
        }
    }

    // Сделайте функцию глобальной, чтобы вызывать из HTML
    window.openRolesModal = openRolesModal;
});