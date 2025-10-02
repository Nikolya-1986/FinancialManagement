function openRolesModal(userId, userRole) {
    document.getElementById('modalUserIdDisplay').textContent = userId; // показываем в modal
    document.getElementById('modalUserId').value = userId;               // скрытое поле
    document.getElementById('roleSelect').value = userRole;              // выбранный ролль
    document.getElementById('selectedRole').value = userRole;            // скрытое поле для новой роли

    var roleSelect = document.getElementById('roleSelect');
    if (roleSelect) {
        roleSelect.value = userRole;
    }

    var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
    myModal.show(); 
}