--Tour Bus To Forbidden Realms
function c89732524.initial_effect(c)
	--search
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(89732524,0))
	e1:SetCategory(CATEGORY_TOHAND)
	e1:SetType(EFFECT_TYPE_FLIP+EFFECT_TYPE_SINGLE)
	e1:SetTarget(c89732524.tg)
	e1:SetOperation(c89732524.op)
	c:RegisterEffect(e1)
end
function c89732524.filter(c)
	return c:IsRace(RACE_FIEND) and not c:IsAttribute(ATTRIBUTE_LIGHT) 
	and not c:IsAttribute(ATTRIBUTE_DARK) and c:IsAbleToHand()
end
function c89732524.tg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return true end
	Duel.SetOperationInfo(0,CATEGORY_TOHAND,nil,1,tp,LOCATION_DECK)
end
function c89732524.op(e,tp,eg,ep,ev,re,r,rp)
	local g=Duel.GetMatchingGroup(c89732524.filter,tp,LOCATION_DECK,0,nil)
	if g:GetCount()>0 and Duel.SelectYesNo(tp,aux.Stringid(89732524,1)) then
		Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_ATOHAND)
		local sg=g:Select(tp,1,1,nil)
		Duel.SendtoHand(sg,nil,REASON_EFFECT)
		Duel.ConfirmCards(1-tp,sg)
	end
end
