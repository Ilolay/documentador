$j = jQuery.noConflict();
$j(document).ready(function () {
   ChangeThemeVariant();
    
   SetStylesheet();
 
   ChangeStylesheet();
 
   FloatThemeBox ();   
});

function ChangeThemeVariant() {
        if ( $j.cookie('theme-variant') == 'light' ) {
            $j('body').removeClass('dark').addClass('light');
        }
        else if ( $j.cookie('theme-variant') == 'dark' ) {
            $j('body').removeClass('light').addClass('dark');
        }
        
        $j('#theme-light').click(function () { 
            $j('body').removeClass('dark').addClass('light'); 
            $j.cookie('theme-variant', 'light', { path: '/' });
            RefreshCufon ();
            return false; 
        });
        
        $j('#theme-dark').click(function () { 
            $j('body').removeClass('light').addClass('dark'); 
            $j.cookie('theme-variant', 'dark', { path: '/' });
            RefreshCufon ();
            return false;
        });
}

function SetStylesheet () {
    if ($j.cookie('stylesheet-color')) {
        $j('#stylesheet-color').attr('href', $j.cookie('stylesheet-color')); 
    }

    if ($j.cookie('stylesheet-bg')) {
        $j('#stylesheet-bg').attr('href', $j.cookie('stylesheet-bg')); 
    }
}

function ChangeStylesheet () {
    
    $j('#sections li a').click(function () {
        sectionName = $j(this).attr('rel');
       
        if ($j('.' + sectionName).is(':visible')) {
            $j('.' + sectionName).hide();
            $j('#page-wrap .homesection:visible:last').addClass('backgroundNone');
        }
        else {
            $j('.' + sectionName).show();
            $j('#page-wrap .homesection').prev('.backgroundNone').removeClass('backgroundNone');
            $j('#page-wrap .homesection:visible:last').addClass('backgroundNone');
        }
            
        return false;
    });
        
    $j('#theme-box a.not').click(function () {        
        /****************
         * Reset
         ****************/
        if ($j(this).attr('id') == 'theme-box-reset') {
            $j.cookie('stylesheet-color', null, { path: '/' });  
            $j.cookie('stylesheet-bg', null, { path: '/' });  
            $j.cookie('theme-variant', null, { path: '/' });  
            
            location.href=$j(this).attr('href');
        }
    
        return false;
    });

    $j('#theme-box a.link').click(function () {        
        /****************
         * Standard Link
         ****************/
        location.href = $j(this).attr('href');
        return false;
    });
    
    $j('#stylesheet-color-setter li a').click(function () {
        path = $j(this).attr('href');        
                
        $j('#stylesheet-color').attr('href', path);
        $j.cookie('stylesheet-color', path, { path: '/' });  

        RefreshCufon ();
                
        return false;
    });
    
    $j('#stylesheet-bg-setter li a').click(function () {
        path = $j(this).attr('href');
                        
        $j('#stylesheet-bg').attr('href', path);        
        $j.cookie('stylesheet-bg', path, { path: '/' });  

        RefreshCufon ();        
        
        return false;
    });
} 

function RefreshCufon () {
    Cufon.refresh('.logo .title');

    Cufon.refresh('h1');    
    Cufon.refresh('h2');
    Cufon.refresh('h3');

    Cufon.refresh('h1 .thin');    
    Cufon.refresh('h2 .thin');
    Cufon.refresh('h3 .thin');   
}

function FloatThemeBox () {
    if ($j.cookie('themebox-status') == 'closed') {
        $j('#theme-box').css({'left': '-116px'});
        $j('#theme-box-closer').removeClass('opened').addClass('closed');       
    }
    
    $j('#theme-box-closer').click(function () {
        if ($j(this).hasClass('opened')) {
            $j('#theme-box').animate({
                'left': '-116px'
            }, 500, function () {
                $j('#theme-box-closer').removeClass('opened').addClass('closed');       
                $j.cookie('themebox-status', 'closed', { path: '/' });
            });
        }
        
        if ($j(this).hasClass('closed')) {
            $j('#theme-box').animate({
                'left': '-0px'
            }, 500, function () {
                $j('#theme-box-closer').removeClass('closed').addClass('opened');       
                $j.cookie('themebox-status', 'opened', { path: '/' });
            });
        }        
    });
    
    var name = '#theme-box';  
    var menuYloc = null; 
    if ($j(name).length) {
        menuYloc = parseInt($j(name).css('top').substring(0,$j(name).css('top').indexOf('px')))      
        $j(window).scroll(function () {
            var offset = menuYloc + $j(document).scrollTop() + 'px';  
            $j(name).animate({top:offset},{duration:500,queue:false});          
        });
    }
}
