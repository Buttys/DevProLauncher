-- 傀儡儀式－パペット・リチューアル
function c1969506.initial_effect(c)
  --Activate
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_SPECIAL_SUMMON)
	e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetCode(EVENT_FREE_CHAIN)
	e1:SetCondition(c1969506.condition)
	e1:SetCost(c1969506.cost)
	e1:SetTarget(c1969506.target)
	e1:SetOperation(c1969506.activate)
	c:RegisterEffect(e1)
end
function c1969506.condition(e,tp,eg,ep,ev,re,r,rp)
	return Duel.GetLP(tp)<=Duel.GetLP(1-tp)-2000
end
function c1969506.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetCurrentPhase()~=PHASE_MAIN2 
	and Duel.GetFlagEffect(tp,1969506)==0 
	end
	local e1=Effect.CreateEffect(e:GetHandler())
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetCode(EFFECT_CANNOT_BP)
	e1:SetProperty(EFFECT_FLAG_PLAYER_TARGET+EFFECT_FLAG_OATH)
	e1:SetTargetRange(1,0)
	e1:SetReset(RESET_PHASE+PHASE_END)
	Duel.RegisterEffect(e1,tp)
	Duel.RegisterFlagEffect(tp,1969506,RESET_PHASE+PHASE_END,EFFECT_FLAG_OATH,1)
end
function c1969506.filter(c,e,tp)
	return c:IsSetCard(0x83) and c:IsType(TYPE_MONSTER) and c:GetLevel()==8 and c:IsCanBeSpecialSummoned(e,0,tp,false,false)
end
function c1969506.target(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chkc then return chkc:IsControler(tp) and chkc:IsLocation(LOCATION_GRAVE) and c1969506.filter(chkc,e,tp)  end
	if chk==0 then return Duel.GetLocationCount(tp,LOCATION_MZONE)>1 and
	Duel.IsExistingTarget(c1969506.filter,tp,LOCATION_GRAVE,0,2,nil,e,tp) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_SPSUMMON)
	local g=Duel.SelectTarget(tp,c1969506.filter,tp,LOCATION_GRAVE,0,2,2,nil,e,tp)
	Duel.SetOperationInfo(0,CATEGORY_SPECIAL_SUMMON,g,2,0,0)
end
function c1969506.activate(e,tp,eg,ep,ev,re,r,rp)
	local g=Duel.GetChainInfo(0,CHAININFO_TARGET_CARDS)
	local sg=g:Filter(Card.IsRelateToEffect,nil,e)
	if sg:GetCount()==0 then return end
	local tg=sg:GetFirst()
	while tg do
		Duel.SpecialSummonStep(tg,0,tp,tp,false,false,POS_FACEUP)
		tg=sg:GetNext()
	end
	Duel.SpecialSummonComplete()
end
