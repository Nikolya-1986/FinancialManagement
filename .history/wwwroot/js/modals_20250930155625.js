function rolesModal(userId, userRole) {
    document.getElementById('modalUserId').textContent = userId;
    document.getElementById('modalUserRole').textContent = userRole;
    var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
    myModal.show();
}