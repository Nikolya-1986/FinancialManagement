function openRolesModal(userId, currentRole) {
    document.getElementById('modalUserIdDisplay').textContent = userId;
    document.getElementById('modalUserId').value = userId;
  
    var roleSelect = document.getElementById('roleSelect');
    roleSelect.value = currentRole;
  
    var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
    myModal.show();
}