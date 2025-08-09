// =========================================================
// Sigorta Takip – Site davranışları
// =========================================================
(function () {
    const root = document.documentElement;
    const body = document.body;
    const sidebarToggle = document.getElementById('sidebarToggle');
    const themeToggle = document.getElementById('themeToggle');

    // ---------- Sidebar state ----------
    try {
        const savedSidebar = localStorage.getItem('sidebar-collapsed');
        if (savedSidebar === '1') body.classList.add('sidebar-collapsed');
    } catch (e) { }

    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function () {
            body.classList.toggle('sidebar-collapsed');
            try {
                localStorage.setItem(
                    'sidebar-collapsed',
                    body.classList.contains('sidebar-collapsed') ? '1' : '0'
                );
            } catch (e) { }
        });
    }

    // ---------- Theme toggle ----------
    function setTheme(t) {
        root.setAttribute('data-bs-theme', t);
        try { localStorage.setItem('theme', t); } catch (e) { }
        updateThemeIcon(t);
    }
    function updateThemeIcon(t) {
        if (!themeToggle) return;
        const i = themeToggle.querySelector('i');
        if (!i) return;
        i.className = t === 'dark' ? 'bi bi-sun' : 'bi bi-moon-stars';
    }

    try {
        const savedTheme = localStorage.getItem('theme');
        if (savedTheme) setTheme(savedTheme);
        else updateThemeIcon(root.getAttribute('data-bs-theme') || 'light');
    } catch (e) { }

    if (themeToggle) {
        themeToggle.addEventListener('click', function () {
            const current = root.getAttribute('data-bs-theme') || 'light';
            setTheme(current === 'light' ? 'dark' : 'light');
        });
    }

    // ---------- Bootstrap yardımcıları ----------
    // Enable tooltips if used
    if (window.bootstrap) {
        const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.forEach(function (el) {
            new bootstrap.Tooltip(el);
        });
    }

    // ---------- Basit arama inputu enter engelle (opsiyonel) ----------
    const searchInputs = document.querySelectorAll('.topbar .search input[type="search"]');
    searchInputs.forEach(inp => {
        inp.addEventListener('keydown', (e) => {
            if (e.key === 'Enter') { e.preventDefault(); }
        });
    });
})();
