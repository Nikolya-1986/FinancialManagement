function openRolesModal(userId, currentRole) {
    document.getElementById('modalUserId').textContent = userId;
    document.getElementById('modalUserId').value = userId;

    document.getElementById('roleSelect').value = currentRole;
    document.getElementById('selectedRole').value = currentRole;

    var roleSelect = document.getElementById('roleSelect');

    roleSelect.onchange = function () {
      document.getElementById('selectedRole').value = this.value;
    }

    var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
    myModal.show();
}