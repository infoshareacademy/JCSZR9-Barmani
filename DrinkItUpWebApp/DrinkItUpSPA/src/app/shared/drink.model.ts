import { DifficultyModel } from "./difficulty.model";
import { IngredientModel } from "./ingredient.model";
import { MainAlcoholModel } from "./mainAlcohol.model";

export class DrinkModel{
    constructor(
        public drinkId: number,
        public name: string,
        public mainAlcohol: MainAlcoholModel,
        public difficulty: DifficultyModel,
        public ingredients: IngredientModel[],
        public description: string,
        public drinkPhotoId: string
    ){}

}