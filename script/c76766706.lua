--渾沌の種
function c76766706.initial_effect(c)
	--add
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_TOHAND)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetCode(EVENT_FREE_CHAIN)
	e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
	e1:SetCondition(c76766706.con)
	e1:SetCost(c76766706.cost)
	e1:SetTarget(c76766706.tg)
	e1:SetOperation(c76766706.op)
	c:RegisterEffect(e1)
end
function c76766706.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetFlagEffect(tp,76766706)==0 end
	Duel.RegisterFlagEffect(tp,76766706,RESET_PHASE+PHASE_END,0,1)
end
function c76766706.tfilter(c,att)
	return c:IsAttribute(att) and c:IsFaceup()
end
function c76766706.con(e)
	return Duel.IsExistingMatchingCard(c76766706.tfilter,tp,LOCATION_MZONE,0,1,nil,ATTRIBUTE_LIGHT) and
		Duel.IsExistingMatchingCard(c76766706.tfilter,tp,LOCATION_MZONE,0,1,nil,ATTRIBUTE_DARK)
end
function c76766706.tg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.IsExistingMatchingCard(c76766706.filter,tp,LOCATION_REMOVED,0,1,nil) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_ATOHAND)
	local g=Duel.SelectTarget(tp,c76766706.filter,tp,LOCATION_REMOVED,0,1,1,nil)
	Duel.SetOperationInfo(0,CATEGORY_TOHAND,nil,1,tp,LOCATION_REMOVED)
end
function c76766706.filter(c)
	return c:IsRace(RACE_WARRIOR) and (c:IsAttribute(ATTRIBUTE_LIGHT) or c:IsAttribute(ATTRIBUTE_DARK)) and c:IsAbleToHand()
end
function c76766706.op(e,tp,eg,ep,ev,re,r,rp)
	local tc=Duel.GetFirstTarget()
	if tc:IsRelateToEffect(e) then
		Duel.SendtoHand(tc,nil,REASON_EFFECT)
	end
end