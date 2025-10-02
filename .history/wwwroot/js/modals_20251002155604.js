function openRolesModal(userId, userRole) {
    document.getElementById('modalUserId').value = userId;
    document.getElementById('modalUserRole').value = userRole;

    var roleSelect = document.getElementById('roleSelect');
    if (roleSelect) {
        roleSelect.value = userRole;value 
    }
    
    var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
    myModal.show();
}