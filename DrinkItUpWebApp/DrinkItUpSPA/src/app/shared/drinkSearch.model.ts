import { DifficultyModel } from "./difficulty.model";
import { MainAlcoholModel } from "./mainAlcohol.model";

export class DrinkSearchModel{
    constructor(
        public drinkId: number,
        public name: string,
        public difficulty: DifficultyModel,
        public mainAlcohol: MainAlcoholModel,
        public description: string,
        public drinkPhotoId: string
    ){}
}