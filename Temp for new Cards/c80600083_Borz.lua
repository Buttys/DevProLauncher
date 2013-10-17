--Noble Knight Borz
function c80600083.initial_effect(c)
	--Attribute Dark
	local e1=Effect.CreateEffect(c)
	e1:SetType(EFFECT_TYPE_SINGLE)
	e1:SetProperty(EFFECT_FLAG_SINGLE_RANGE)
	e1:SetCode(EFFECT_CHANGE_TYPE)
	e1:SetRange(LOCATION_MZONE)
	e1:SetCondition(c80600083.eqcon1)
	e1:SetValue(TYPE_NORMAL+TYPE_MONSTER)
	c:RegisterEffect(e1)
	--Attribute Dark
	local e2=Effect.CreateEffect(c)
	e2:SetType(EFFECT_TYPE_SINGLE)
	e2:SetProperty(EFFECT_FLAG_SINGLE_RANGE)
	e2:SetCode(EFFECT_CHANGE_ATTRIBUTE)
	e2:SetRange(LOCATION_MZONE)
	e2:SetCondition(c80600083.eqcon2)
	e2:SetValue(ATTRIBUTE_DARK)
	c:RegisterEffect(e2)
	local e3=Effect.CreateEffect(c)
	e3:SetType(EFFECT_TYPE_SINGLE)
	e3:SetProperty(EFFECT_FLAG_SINGLE_RANGE)
	e3:SetCode(EFFECT_UPDATE_LEVEL)
	e3:SetRange(LOCATION_MZONE)
	e3:SetCondition(c80600083.eqcon2)
	e3:SetValue(1)
	c:RegisterEffect(e3)
	--Search
	local e4=Effect.CreateEffect(c)
	e4:SetDescription(aux.Stringid(80600083,0))
	e4:SetCategory(CATEGORY_TOHAND)
	e4:SetType(EFFECT_TYPE_IGNITION)
	e4:SetRange(LOCATION_MZONE)
	e4:SetCondition(c80600083.con)
	e4:SetCost(c80600083.cost)
	e4:SetTarget(c80600083.tg)
	e4:SetOperation(c80600083.op)
	c:RegisterEffect(e4)
end
function c80600083.eqcon1(e)
	local eg=e:GetHandler():GetEquipGroup()
	return not eg or not eg:IsExists(Card.IsSetCard,1,nil,0x207a)
end
function c80600083.eqcon2(e)
	local eg=e:GetHandler():GetEquipGroup()
	return eg and eg:IsExists(Card.IsSetCard,1,nil,0x207a)
end
function c80600083.con(e,tp,eg,ep,ev,re,r,rp)
	return c80600083.eqcon2(e)
end
function c80600083.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetFlagEffect(tp,80600083)==0 end
	Duel.RegisterFlagEffect(tp,80600083,RESET_PHASE+PHASE_END,0,1)
end
function c80600083.filter1(c)
	return c:IsSetCard(0x207a) and c:IsAbleToHand()
end
function c80600083.tg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then
		local g=Duel.GetMatchingGroup(c80600083.filter1,tp,LOCATION_DECK,0,nil)
		return g:GetCount()>=3
	end
	Duel.SetOperationInfo(0,CATEGORY_TOHAND,nil,1,0,LOCATION_DECK)
end
function c80600083.op(e,tp,eg,ep,ev,re,r,rp)
	local g=Duel.SelectMatchingCard(tp,c80600083.filter1,tp,LOCATION_DECK,0,3,3,nil,e,tp)
	if g:GetCount()>=3 then
		Duel.ConfirmCards(1-tp,g)
		Duel.ShuffleDeck(tp)
		Duel.Hint(HINT_SELECTMSG,1-tp,HINTMSG_ATOHAND)
		local tc=g:Select(1-tp,1,1,nil):GetFirst()
		if tc:IsAbleToHand() then
			Duel.SendtoHand(tc,nil,REASON_EFFECT)
		else
			Duel.SendtoGrave(tc,REASON_EFFECT)
		end
	end
end
