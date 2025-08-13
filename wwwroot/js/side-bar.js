(function () {
    const sidenav = document.getElementById('mySidenav');
    const content = document.getElementById('content');
    const btnOpen = document.getElementById('openSidebarBtn');
    const btnClose = sidenav.querySelector('.closebtn');

    function openNav() {
        sidenav.classList.add('open');
        if (window.matchMedia('(min-width: 768px)').matches) {
            content.classList.add('shifted');
        }
    }

    function closeNav() {
        sidenav.classList.remove('open');
        content.classList.remove('shifted');
    }

    btnOpen.addEventListener('click', openNav);
    btnClose.addEventListener('click', closeNav);

    // Close on ESC
    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') closeNav();
    });

    // Close when clicking outside on mobile
    document.addEventListener('click', (e) => {
        if (!sidenav.classList.contains('open')) return;
        const clickedInside = sidenav.contains(e.target) || btnOpen.contains(e.target);
        if (!clickedInside && window.matchMedia('(max-width: 767.98px)').matches) {
            closeNav();
        }
    });
})();
