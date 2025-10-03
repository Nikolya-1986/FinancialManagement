function openRolesModal(userId, currentRole) {
    var userIdSpan = document.getElementById('modalUserIdDisplay');
    var userIdInput = document.getElementById('modalUserId');
    var select = document.getElementById('roleSelect');

    if (userIdSpan && userIdInput && select) {
      userIdSpan.textContent = userId;
      userIdInput.value = userId;
      select.value = currentRole;
      
      document.getElementById('selectedRole').value = currentRole;

      var myModal = new bootstrap.Modal(document.getElementById('roleModal'));
      myModal.show();
    } else {
      console.error('Некоторые элементы не найдены');
    }
  }