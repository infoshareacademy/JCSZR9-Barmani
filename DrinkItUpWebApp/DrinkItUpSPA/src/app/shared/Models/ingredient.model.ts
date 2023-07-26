import { UnitModel } from "./unit.model";

export class IngredientModel{
    constructor(
        public ingredientId: number,
        public name: string,
        public unit: UnitModel,
        public quantity?: number,
        public isUsed?: boolean
    ){}
}