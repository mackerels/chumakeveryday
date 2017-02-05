export class FaceProvider
{
    private _element: JQuery;
    private _parent: JQuery;

    constructor()
    {
        this._element = $('.chumak-container');
        this._parent = $('.base');
    }

    set face(image: Blob)
    {
        this._element.attr('src', URL.createObjectURL(image));
        this._element.attr('height', `${this._parent.height()}px`);
    }
}