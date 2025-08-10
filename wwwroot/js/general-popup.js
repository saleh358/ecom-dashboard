 (function () {
  const root = document.getElementById('general-popup');
    const textEl = document.getElementById('gp-text');
    const btnOk = document.getElementById('gp-ok');
    const btnCancel = document.getElementById('gp-cancel');
    const btnClose = root.querySelector('.gp-close');

    let onOk = null, onClose = null;

    function open(message, okCb, closeCb) {
        textEl.textContent = message || '';
    onOk = typeof okCb === 'function' ? okCb : null;
    onClose = typeof closeCb === 'function' ? closeCb : null;
    root.hidden = false;
  }

    function close() {
        root.hidden = true;
    if (onClose) onClose();
  }

    btnOk.addEventListener('click', function () {
    if (onOk) onOk();
    close();
  });
    btnCancel.addEventListener('click', close);
    btnClose.addEventListener('click', close);
    root.addEventListener('click', function (e) {
    if (e.target === root) close();
  });

    window.GeneralPopup = {open, close};
})();
