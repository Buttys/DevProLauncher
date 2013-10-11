--Ｓｐ－リミッター解除
function c100100502.initial_effect(c)
	--Activate
	local e1=Effect.CreateEffect(c)
	e1:SetCategory(CATEGORY_ATKCHANGE)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetProperty(EFFECT_FLAG_DAMAGE_STEP)
	e1:SetCode(EVENT_FREE_CHAIN)
	e1:SetHintTiming(TIMING_DAMAGE_STEP)
	e1:SetCondition(c100100502.condition)
	e1:SetCost(c100100502.cost)
	e1:SetTarget(c100100502.target)
	e1:SetOperation(c100100502.activate)
	c:RegisterEffect(e1)
end
function c100100502.condition(e,tp,eg,ep,ev,re,r,rp)
	return Duel.GetCurrentPhase()~=PHASE_DAMAGE or not Duel.IsDamageCalculated() 
end
function c100100502.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	local tc=Duel.GetFieldCard(tp,LOCATION_SZONE,5)
	if chk==0 then return tc and tc:IsCanRemoveCounter(tp,0x91,6,REASON_COST) end	 
	Duel.Hint(HINT_OPSELECTED,1-tp,e:GetDescription())
	tc:RemoveCounter(tp,0x91,6,REASON_COST)	
end
function c100100502.filter(c)
	return c:IsFaceup() and c:IsRace(RACE_MACHINE)
end
function c100100502.target(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.IsExistingMatchingCard(c100100502.filter,tp,LOCATION_MZONE,0,1,nil) end
end
function c100100502.activate(e,tp,eg,ep,ev,re,r,rp)
	local sg=Duel.GetMatchingGroup(c100100502.filter,tp,LOCATION_MZONE,0,nil)
	local c=e:GetHandler()
	local tc=sg:GetFirst()
	while tc do
		local e1=Effect.CreateEffect(c)
		e1:SetType(EFFECT_TYPE_SINGLE)
		e1:SetCode(EFFECT_SET_ATTACK_FINAL)
		e1:SetReset(RESET_EVENT+0x1fe0000+RESET_PHASE+RESET_END)
		e1:SetValue(tc:GetAttack()*2)
		tc:RegisterEffect(e1)
		local e2=Effect.CreateEffect(c)
		e2:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_CONTINUOUS)
		e2:SetCode(EVENT_PHASE+PHASE_END)
		e2:SetRange(LOCATION_MZONE)
		e2:SetReset(RESET_EVENT+0x1fe0000+RESET_PHASE+RESET_END)
		e2:SetCountLimit(1)
		e2:SetOperation(c100100502.desop)
		tc:RegisterEffect(e2)
		tc=sg:GetNext()
	end
end
function c100100502.desop(e,tp,eg,ep,ev,re,r,rp)
	Duel.Destroy(e:GetHandler(),REASON_EFFECT)
end
