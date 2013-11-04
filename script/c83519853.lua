--Ignoble Knight of High Laundsallyn
function c83519853.initial_effect(c)
	--synchro summon
	aux.AddSynchroProcedure(c,nil,aux.NonTuner(Card.IsSetCode,0x7a),1)
	c:EnableReviveLimit()
	c:SetUniqueOnField(1,0,83519853)
	--equip
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(83519853,0))
	e1:SetCategory(CATEGORY_EQUIP)
	e1:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_O)
	e1:SetCode(EVENT_SPSUMMON_SUCCESS)
	e1:SetCondition(c83519853.eqcon)
	e1:SetTarget(c83519853.eqtg)
	e1:SetOperation(c83519853.eqop)
	c:RegisterEffect(e1)
	--To Hand
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(83519853,0))
	e2:SetCategory(CATEGORY_TOHAND)
	e2:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_TRIGGER_O)
	e2:SetCode(EVENT_PHASE+PHASE_BATTLE)
	e2:SetRange(LOCATION_MZONE)
	e2:SetCountLimit(1)
	e2:SetCondition(c83519853.addcon)
	e2:SetTarget(c83519853.addtg)
	e2:SetOperation(c83519853.addop)
	c:RegisterEffect(e2)
	local e3=Effect.CreateEffect(c)
	e3:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_CONTINUOUS)
	e3:SetCode(EVENT_BATTLE_DESTROYING)
	e3:SetCondition(c83519853.condition)
	e3:SetOperation(c83519853.regop)
	c:RegisterEffect(e3)
end
function c83519853.eqcon(e,tp,eg,ep,ev,re,r,rp)
	return e:GetHandler():GetSummonType()==SUMMON_TYPE_SYNCHRO
end
function c83519853.filter(c,ec)
	return c:IsType(TYPE_EQUIP) and c:IsSetCard(0x207a) and c:CheckEquipTarget(ec)
end
function c83519853.eqtg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetLocationCount(tp,LOCATION_SZONE)>0
		and Duel.IsExistingMatchingCard(c83519853.filter,tp,LOCATION_DECK,0,1,nil,e:GetHandler()) end
	Duel.SetOperationInfo(0,CATEGORY_EQUIP,nil,1,tp,LOCATION_DECK)
end
function c83519853.eqop(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	if Duel.GetLocationCount(tp,LOCATION_SZONE)<=0 or c:IsFacedown() or not c:IsRelateToEffect(e) then return end
	Duel.Hint(HINT_SELECTMSG,tp,aux.Stringid(83519853,2))
	local g=Duel.SelectMatchingCard(tp,c83519853.filter,tp,LOCATION_DECK,0,1,1,nil,c)
	if g:GetCount()>0 then
		Duel.Equip(tp,g:GetFirst(),c)
	end
end
function c83519853.addcon(e,tp,eg,ep,ev,re,r,rp)
	return e:GetHandler():GetFlagEffect(83519853)~=0
end
function c83519853.addfilter(c)
	return (c:IsSetCard(0x7a) or c:IsSetCard(0x207a)) and c:IsAbleToHand()
end
function c83519853.addtg(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chk==0 then return Duel.IsExistingMatchingCard(c83519853.addfilter,tp,LOCATION_DECK,0,1,nil) end
	Duel.SetOperationInfo(0,CATEGORY_TOHAND,nil,1,tp,LOCATION_DECK)
end
function c83519853.addop(e,tp,eg,ep,ev,re,r,rp)
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_ATOHAND)
	local g=Duel.SelectMatchingCard(tp,c83519853.addfilter,tp,LOCATION_DECK,0,1,1,nil)
	if g:GetCount()>0 then
		Duel.SendtoHand(g,nil,REASON_EFFECT)
		Duel.ConfirmCards(1-tp,g)
	end
end
function c83519853.condition(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	local a=Duel.GetAttacker()
	local d=Duel.GetAttackTarget()
	if a~=c then d=a end
	return c:IsRelateToBattle() and c:IsFaceup()
		and d and d:GetLocation()==LOCATION_GRAVE and d:IsType(TYPE_MONSTER)
end
function c83519853.regop(e,tp,eg,ep,ev,re,r,rp)
	e:GetHandler():RegisterFlagEffect(83519853,RESET_EVENT+0x1fe0000+RESET_PHASE+PHASE_BATTLE,0,1)
end