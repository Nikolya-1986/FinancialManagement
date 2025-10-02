function openRolesModal(userId, userRole) {
    document.getElementById('modalUserId').value = userId;
    document.getElementById('roleSelect').value = userRole;

    // Обновляем скрытые поля
    document.getElementById('selectedRole').value = userRole;

    var roleSelect = document.getElementById('roleSelect');
    if (roleSelect) {
        roleSelect.value = userRole;
    }

    var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
    myModal.show(); 
}