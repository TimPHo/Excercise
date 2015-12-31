var index = (function () {
    var me = {};
    me.DOM = {},

    me.Initialize = function () {
        this.DOM.displayArea = $('#displayArea');
        this.DOM.deckType = $('#deckType');
        this.DOM.appStatus = $('#status');
    };

    me.NewDeck = function () {
        var id = $(me.DOM.deckType).val();
        me.Ajax('/Home/NewDeck/' + id);
    };

    me.SortDeck = function () {
        me.Ajax('/Home/Sort');
    };

    me.ShuffleDeck = function () {
        me.Ajax('/Home/Shuffle');
    };

    me.Ajax = function (url) {
        me.DOM.appStatus.html('Loading...');
        $.get(url).success(function (data) {
            me.DOM.appStatus.html('');
            $(me.DOM.displayArea).html(data);
        }).fail(function () {
            me.DOM.appStatus.html('An error occured while loading page!');
        });;
    };


    return me;
})();


$(document).ready(function () {
    index.Initialize();
    $('#optionsArea')
        .delegate("#newBttn", 'click', index.NewDeck)
        .delegate("#sortBttn", 'click', index.SortDeck)
        .delegate("#shuffleBttn", 'click', index.ShuffleDeck);
});

