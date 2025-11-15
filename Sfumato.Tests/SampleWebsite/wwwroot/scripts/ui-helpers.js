let rootEl;
let pageOverlayEl;

let navBarEl;
let sidebarEl;
let contentEl;
let heroEl;
let indexPageWrapperEl;

let mobileMenuEl;
let mobileMenuButtonEl;
let mobileMenuCloseButtonEl;

let mobileIndicatorEl;

let isMobile = false;

let anchorLinkEls = [];

window.closeOverlay = () => {

    if (!pageOverlayEl.classList.contains('max-lg:w-screen')) return;

    window.requestAnimationFrame(function() {

        pageOverlayEl.classList.remove('max-lg:w-screen');
        pageOverlayEl.classList.remove('max-lg:h-screen');

    });
}

window.openOverlay = () => {

    if (pageOverlayEl.classList.contains('max-lg:w-screen')) return;

    window.requestAnimationFrame(function() {

        pageOverlayEl.classList.add('max-lg:w-screen');
        pageOverlayEl.classList.add('max-lg:h-screen');

    });
}

window.toggleOverlay = () => {

    window.requestAnimationFrame(function() {

        pageOverlayEl.classList.toggle('max-lg:w-screen');
        pageOverlayEl.classList.toggle('max-lg:h-screen');

    });
}

window.handlePageFreeze = () => {

    if (window.areMenusOpen()) {

        window.frozenScrollY = window.scrollY;

        if (document.documentElement.style.overflow !== 'hidden') {
            document.documentElement.style.overflow = 'hidden';
            rootEl.style.overflow = 'hidden';
        }

        window.openOverlay();

    } else {

        if (document.documentElement.style.overflow !== '') {
            document.documentElement.style.overflow = '';
            rootEl.style.overflow = '';
        }

        window.scrollTo(0, window.frozenScrollY || 0);

        window.closeOverlay();
    }
}

window.updatePageFreeze = () => {

    if (window.areMenusOpen()) {

        if (document.documentElement.style.overflow !== 'hidden') {
            document.documentElement.style.overflow = 'hidden';
            rootEl.style.overflow = 'hidden';
        }

        window.openOverlay();

    } else {

        if (document.documentElement.style.overflow !== '') {
            document.documentElement.style.overflow = '';
            rootEl.style.overflow = '';
        }

        window.closeOverlay();
    }
}

window.areMenusOpen = () => {

    if (!isMobile)
        return false;

    let result = false;

    if (mobileMenuEl) {
        result = mobileMenuEl.classList.contains('max-lg:left-999') === false;
    }

    if (sidebarEl && result === false) {
        result = sidebarEl.classList.contains('max-lg:left-999') === false;
    }

    return result;
}

window.closeMenus = () => {

    if (!window.areMenusOpen())
        return;

    window.closeMobileMenu();
    window.closeSidebarMenu();
}

window.closeMobileMenu = () => {

    window.requestAnimationFrame(function() {

        if (mobileMenuEl && !mobileMenuEl.classList.contains('max-lg:left-999')) {
            mobileMenuEl.classList.add('max-lg:left-999');
        }

        window.handlePageFreeze();
    });
}

window.toggleMobileMenu = () => {

    window.requestAnimationFrame(function() {

        if (mobileMenuEl) {
            mobileMenuEl.classList.toggle('max-lg:left-999');
        }

        window.handlePageFreeze();
    });
}

window.closeSidebarMenu = () => {

    window.requestAnimationFrame(function() {

        if (sidebarEl && !sidebarEl.classList.contains('max-lg:left-999')) {
            sidebarEl.classList.add('max-lg:left-999');
        }

        window.handlePageFreeze();
    });
}

window.toggleSidebar = () => {

    window.requestAnimationFrame(function() {

        if (sidebarEl) {
            sidebarEl.classList.toggle('max-lg:left-999');
        }

        window.handlePageFreeze();
    });
}

window.setScrollBarWidth = () => {

    setTimeout(function() {

        const scrollDiv = document.createElement('div');

        scrollDiv.style.visibility = 'hidden';
        scrollDiv.style.overflow = 'scroll';
        scrollDiv.style.position = 'absolute';
        scrollDiv.style.top = '-9999px';
        scrollDiv.style.width = '100px';
        scrollDiv.style.height = '100px';

        document.body.appendChild(scrollDiv);

        const innerDiv = document.createElement('div');

        innerDiv.style.width = '100%';
        innerDiv.style.height = '100%';
        scrollDiv.appendChild(innerDiv);

        const scrollbarWidth = scrollDiv.offsetWidth - scrollDiv.clientWidth;

        document.documentElement.style.setProperty("--scrollbar-padding", `${scrollbarWidth}px`);
        document.body.removeChild(scrollDiv);

    }, 0);
}

window.updateIsMobileFlag = () => {

    if (!mobileIndicatorEl)
        return;

    isMobile = mobileIndicatorEl.clientWidth === 0;

    window.requestAnimationFrame(function() {
        window.updatePageFreeze();
    });
}

window.restoreSidebarScrollPosition = () => {

    if (sidebarEl) {

        const saved = localStorage.getItem('sidebarScrollTop');

        if (saved) {

            try {

                const { value, expires } = JSON.parse(saved);

                if (!expires || Date.now() < expires) {
                    sidebarEl.scrollTop = parseInt(value, 10);
                } else {
                    localStorage.removeItem('sidebarScrollTop');
                }

            } catch {

                sidebarEl.scrollTop = parseInt(saved, 10);
            }
        }
    }
}

window.saveSidebarScrollPosition = () => {

    if (sidebarEl) {

        const expires = Date.now() + 30 * 60 * 1000; // 30 minutes
        const data = JSON.stringify({ value: sidebarEl.scrollTop, expires });

        localStorage.setItem('sidebarScrollTop', data);
    }
}

window.setSelectedHash = (newHash = ``) => {

    window.requestAnimationFrame(function() {

        for (const anchor of anchorLinkEls) {
            anchor.classList.remove(`text-primary!`);
            anchor.classList.remove(`dark:text-secondary!`);
        }

        let hash = newHash === `` ? window.location.hash : newHash;

        if (hash === ``) hash = `#top`;

        const hashEl = document.querySelector(`a[href="${hash}"]`);

        if (hashEl) {
            hashEl.classList.add(`text-primary!`);
            hashEl.classList.add(`dark:text-secondary!`);
        }
    });
}

window.isElementInViewport = (el) => {

    const rect = el.getBoundingClientRect();

    return (
        rect.top >= 0 && rect.bottom <= (window.innerHeight || document.documentElement.clientHeight)
    );
}

window.updateHashOnScroll = () => {

    let found = false;

    const candidates = [];

    for (const anchor of anchorLinkEls) {

        const hash = anchor.getAttribute('href');

        if (!hash || hash === '#') continue;

        const target = document.getElementById(hash.slice(1));

        if (!target) continue;

        const rect = target.getBoundingClientRect();

        if (isElementInViewport(target)) {

            if (window.location.hash !== hash) {
                history.replaceState(null, '', hash);
                window.setSelectedHash(hash);
            }

            found = true;
            break;
        }

        candidates.push({ hash, rect });
    }

    if (!found) {

        const above = candidates.filter(c => c.rect.top < 0);

        if (above.length) {

            const nearest = above.reduce((prev, curr) =>
                curr.rect.top > prev.rect.top ? curr : prev
            );

            if (window.location.hash !== nearest.hash) {
                history.replaceState(null, '', nearest.hash);
                window.setSelectedHash(nearest.hash);
            }
        }
    }
}

window.throttleEvent = (fn, wait) => {

    let lastTime = 0;

    return function(...args) {

        const now = Date.now();

        if (now - lastTime >= wait) {
            lastTime = now;
            fn.apply(this, args);
        }
    };
}

_ = halide.core.documentReadyAsync(async () => {

    rootEl = document.querySelector(`body`);
    pageOverlayEl = document.getElementById(`pageOverlay`);

    navBarEl = document.getElementById(`nav-bar`);
    sidebarEl = document.getElementById(`sidebar-wrapper`);
    contentEl = document.getElementById(`content-wrapper`);
    heroEl = document.getElementById(`hero-wrapper`);
    indexPageWrapperEl = document.getElementById(`index-page-wrapper`);

    mobileMenuEl = document.getElementById(`mobileMenu`);
    mobileMenuButtonEl = document.getElementById(`mobileMenuButton`);
    mobileMenuCloseButtonEl = document.getElementById(`mobileMenuCloseButton`);

    mobileIndicatorEl = document.getElementById(`mobile-indicator`);

    window.updateIsMobileFlag();
    window.restoreSidebarScrollPosition();
    window.setSelectedHash();

    document.querySelectorAll('a[href^="#"]').forEach(el => {
        anchorLinkEls.push(el);
    });
    
    window.addEventListener('resize', window.throttleEvent(window.updateIsMobileFlag, 100));
    window.addEventListener('beforeunload', window.saveSidebarScrollPosition);
    window.addEventListener('scroll', window.throttleEvent(window.updateHashOnScroll, 100), { passive: true });
    
    document.addEventListener('click', function (e) {

        const link = e.target.closest('a[href^="#"]');

        if (link && link.hash) {

            const targetId = link.hash.slice(1);
            const targetEl = document.getElementById(targetId);

            if (targetEl) {

                e.preventDefault();

                const offset = 7 * parseFloat(getComputedStyle(document.documentElement).fontSize); // 4.5rem nav bar height + 2.5rem unit padding-y
                const top = targetEl.getBoundingClientRect().top + window.scrollY - offset;

                window.scrollTo({ top });
                history.pushState(null, '', e.target.hash);
            }

            window.setSelectedHash(e.target.hash);
        }
    });
});