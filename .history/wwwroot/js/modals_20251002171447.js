function openRolesModal(userId, currentRole) {
    document.getElementById('modalUserId').value = userId;
    var roleSelect = document.getElementById('roleSelect');
    roleSelect.value = currentRole;
    document.getElementById('selectedRole').value = currentRole;

    roleSelect.onchange = function () {
      document.getElementById('selectedRole').value = this.value;
    };

    var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
    myModal.show();
}