import { DifficultyModel } from "./difficulty.model";
import { IngredientModel } from "./ingredient.model";
import { MainAlcoholModel } from "./mainAlcohol.model";

export class DrinkModel{
    constructor(
        public drinkId?: number,
        public name?: string,
        public mainAlcoholId?: number,
        public mainAlcohol?: MainAlcoholModel,
        public difficultyId?: number,
        public difficulty?: DifficultyModel,
        public ingredients?: IngredientModel[],
        public description?: string,
        public drinkPhotoId?: string
    ){}

}