--Box of Friends
function c81587028.initial_effect(c)
  --spsummon
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(81587028,1))
	e2:SetCategory(CATEGORY_SPECIAL_SUMMON)
	e2:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_O)
	e2:SetProperty(EFFECT_FLAG_DAMAGE_STEP+EFFECT_FLAG_DELAY)
	e2:SetCode(EVENT_TO_GRAVE)
	e2:SetCondition(c81587028.spcon)
	e2:SetCost(c81587028.spcost)
	e2:SetTarget(c81587028.sptg)
	e2:SetOperation(c81587028.spop)
	c:RegisterEffect(e2)
end
function c81587028.spcon(e,tp,eg,ep,ev,re,r,rp)
	return e:GetHandler():IsReason(REASON_DESTROY)
end
function c81587028.spcost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetFlagEffect(tp,81587028)==0 end
	Duel.RegisterFlagEffect(tp,81587028,RESET_PHASE+PHASE_END,EFFECT_FLAG_OATH,1)
end
function c81587028.filter(c,e,tp)
	return c:IsType(TYPE_NORMAL) and (c:GetAttack()==0 or c:GetDefence()==0) and c:IsCanBeSpecialSummoned(e,0,tp,false,false)
	and Duel.IsExistingMatchingCard(c81587028.filter2,tp,LOCATION_DECK,0,1,nil,e,tp,c:GetCode())
end
function c81587028.filter2(c,e,tp,code)
	return c:IsType(TYPE_NORMAL) and (c:GetAttack()==0 or c:GetDefence()==0) and c:IsCanBeSpecialSummoned(e,0,tp,false,false)
	and not c:IsCode(code)
end
function c81587028.sptg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then
		return Duel.GetLocationCount(tp,LOCATION_MZONE)>1 and
		Duel.IsExistingMatchingCard(c81587028.filter,tp,LOCATION_DECK,0,1,nil,e,tp)
	end
	Duel.SetOperationInfo(0,CATEGORY_SPECIAL_SUMMON,nil,2,tp,LOCATION_DECK)
end
function c81587028.spop(e,tp,eg,ep,ev,re,r,rp)
	if Duel.GetLocationCount(tp,LOCATION_MZONE)<2 then return end
	if not Duel.IsExistingMatchingCard(c81587028.filter,tp,LOCATION_DECK,0,1,nil,e,tp) then return end
	local tc1=Duel.SelectMatchingCard(tp,c81587028.filter,tp,LOCATION_DECK,0,1,1,nil,e,tp):GetFirst()
	local tc2=Duel.SelectMatchingCard(tp,c81587028.filter2,tp,LOCATION_DECK,0,1,1,nil,e,tp,tc1:GetCode()):GetFirst()
	local sg=Group.FromCards(tc1,tc2)

	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_SPSUMMON)
	Duel.SpecialSummonStep(tc1,0,tp,tp,false,false,POS_FACEUP_DEFENCE)
	Duel.SpecialSummonStep(tc2,0,tp,tp,false,false,POS_FACEUP_DEFENCE)
	Duel.SpecialSummonComplete()
	sg:KeepAlive()
	local e1=Effect.CreateEffect(e:GetHandler())
	e1:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_CONTINUOUS)
	e1:SetProperty(EFFECT_FLAG_CANNOT_DISABLE)
	e1:SetCode(EVENT_PHASE+PHASE_END)
	e1:SetCountLimit(1)
	e1:SetLabelObject(sg)
	e1:SetCondition(c81587028.descon)
	e1:SetOperation(c81587028.desop)
	if Duel.GetCurrentPhase()==PHASE_END and Duel.GetTurnPlayer()==tp then
		e1:SetLabel(1)
		e1:SetReset(RESET_PHASE+PHASE_END+RESET_SELF_TURN,2)
		tc1:RegisterFlagEffect(81587028,RESET_EVENT+0x1fe0000+RESET_PHASE+PHASE_END+RESET_SELF_TURN,0,2)
		tc2:RegisterFlagEffect(81587028,RESET_EVENT+0x1fe0000+RESET_PHASE+PHASE_END+RESET_SELF_TURN,0,2)
	else
		e1:SetReset(RESET_PHASE+PHASE_END+RESET_SELF_TURN)
		tc1:RegisterFlagEffect(81587028,RESET_EVENT+0x1fe0000+RESET_PHASE+PHASE_END+RESET_SELF_TURN,0,1)
		tc2:RegisterFlagEffect(81587028,RESET_EVENT+0x1fe0000+RESET_PHASE+PHASE_END+RESET_SELF_TURN,0,1)
	end
	Duel.RegisterEffect(e1,tp)
	local e2=Effect.CreateEffect(e:GetHandler())
	e2:SetType(EFFECT_TYPE_SINGLE)
	e2:SetCode(EFFECT_CANNOT_BE_SYNCHRO_MATERIAL)
	e2:SetProperty(EFFECT_FLAG_UNCOPYABLE)
	e2:SetValue(1)
	e2:SetReset(RESET_EVENT+0x1fe0000)
	tc1:RegisterEffect(e2,true)
	tc2:RegisterEffect(e2,true)
end
function c81587028.descon(e,tp)
	return Duel.GetTurnPlayer()==tp
end
function c81587028.desfilter(c)
	return c:GetFlagEffect(81587028)>0
end
function c81587028.desop(e,tp,eg,ep,ev,re,r,rp)
	if e:GetLabel()==0 then
		local g=e:GetLabelObject()
		local tg=g:Filter(c81587028.desfilter,nil)
		g:DeleteGroup()
		Duel.Destroy(tg,REASON_EFFECT)
	else
		e:SetLabel(0)
	end
end
