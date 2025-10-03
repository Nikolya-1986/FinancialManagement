function openRolesModal(userId, userRole) {
    document.getElementById('modalUserIdDisplay').textContent = userId;
    document.getElementById('modalUserId').value = userId;
    document.getElementById('modalUserRole').textContent = userRole;

    var roleSelect = document.getElementById('roleSelect');
    if (roleSelect) {
        roleSelect.value = userRole;
    }

    var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
    myModal.show(); 
}