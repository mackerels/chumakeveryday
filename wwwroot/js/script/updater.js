$(document)
    .ready(function() {
        $('.rand')
            .click(function() {
                var base = $('.base');
                base.css(
                    'background-image',
                    'url(\'/random?random=' + new Date().getTime() + '\')'
                );
            });
    });