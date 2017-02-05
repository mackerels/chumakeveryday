export class ButtonsProvider
{
    private _buttonRandom: JQuery;

    constructor()
    {
        this._buttonRandom = $('.rand');
    }

    set random(closure: () => void)
    {
        let internalClosure = async() =>
        {
            this._buttonRandom.off('click');
            await closure();
            this._buttonRandom.click(internalClosure);
        };

        this._buttonRandom.click(internalClosure);
    }
}