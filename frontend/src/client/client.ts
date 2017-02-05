import {FaceProvider} from '../view/face.provider';
import {ButtonsProvider} from '../view/buttons.provider';

export class Client
{
    private DAILY_PATH: string = '/daily';
    private RANDOM_PATH: string = '/random';

    private _chumak: FaceProvider;
    private _buttons: ButtonsProvider;

    constructor()
    {
        this._chumak = new FaceProvider();
        this._buttons = new ButtonsProvider();
    }

    public async init()
    {
        this._buttons.random = async() =>
            this._chumak.face = await this.loadRandom();

        this._chumak.face = await this.loadDaily();
    }

    private async loadDaily(): Promise<Blob>
    {
        let response = await fetch(this.DAILY_PATH);
        return await response.blob();
    }

    private async loadRandom(): Promise<Blob>
    {
        let response = await fetch(this.RANDOM_PATH);
        return await response.blob();
    }
}