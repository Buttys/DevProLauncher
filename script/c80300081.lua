--Sylvan Mikorange
function c80300081.initial_effect(c)
	--deck check
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(80300081,0))
	e1:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_O)
	e1:SetProperty(EFFECT_FLAG_DAMAGE_STEP)
	e1:SetCode(EVENT_TO_GRAVE)
	e1:SetCondition(c80300081.condition)
	e1:SetTarget(c80300081.target)
	e1:SetOperation(c80300081.operation)
	c:RegisterEffect(e1)
	--boost
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(80300081,1))
	e2:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_F)
	e2:SetCategory(CATEGORY_ATKCHANGE+CATEGORY_DEFCHANGE)
	e2:SetCode(EVENT_TO_GRAVE)
	e2:SetProperty(EFFECT_FLAG_DELAY)
	e2:SetCondition(c80300081.tdcon)
	e2:SetOperation(c80300081.tdop)
	c:RegisterEffect(e2)
	if not c80300081.global_check then
		c80300081.global_check=true
		c80300081[0]=Group.CreateGroup()
		c80300081[0]:KeepAlive()
		c80300081[1]=0
		local ge1=Effect.CreateEffect(c)
		ge1:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_CONTINUOUS)
		ge1:SetCode(EVENT_CONFIRM_DECKTOP)
		ge1:SetOperation(c80300081.checkop)
		Duel.RegisterEffect(ge1,0)
	end
end
function c80300081.checkop(e,tp,eg,ep,ev,re,r,rp)
	c80300081[0]:Clear()
	c80300081[0]:Merge(eg)
	c80300081[1]=re
end
function c80300081.condition(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	return c:IsReason(REASON_DESTROY) and c:GetReasonPlayer()~=tp
		and c:GetPreviousControler()==tp and c:IsPreviousLocation(LOCATION_ONFIELD)
end
function c80300081.target(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.GetFieldGroupCount(tp,LOCATION_DECK,0)>0 end
end
function c80300081.operation(e,tp,eg,ep,ev,re,r,rp)
	if Duel.GetFieldGroupCount(tp,LOCATION_DECK,0)==0 then return end
	Duel.ConfirmDecktop(tp,1)
	local g=Duel.GetDecktopGroup(tp,1)
	local tc=g:GetFirst()
	if tc:IsRace(RACE_PLANT) then
		Duel.DisableShuffleCheck()
		Duel.SendtoGrave(g,REASON_EFFECT)
	else
		Duel.MoveSequence(tc,1)
	end
end
function c80300081.tdcon(e,tp,eg,ep,ev,re,r,rp)
	return re and e:GetHandler():IsPreviousLocation(LOCATION_DECK)
		and c80300081[0]:IsContains(e:GetHandler()) and c80300081[1]==re
end
function c80300081.filter(c)
	return c:IsFaceup() and c:IsRace(RACE_PLANT)
end
function c80300081.tdop(e,tp,eg,ep,ev,re,r,rp)
	local g=Duel.GetMatchingGroup(c80300081.filter,tp,LOCATION_MZONE,0,nil)
	tc=g:GetFirst()
	if not tc then return end
	while tc do
		local e1=Effect.CreateEffect(e:GetHandler())
		e1:SetType(EFFECT_TYPE_SINGLE)
		e1:SetCode(EFFECT_UPDATE_ATTACK)
		e1:SetValue(300)
		e1:SetReset(RESET_EVENT+0x1fe0000)
		tc:RegisterEffect(e1)
		local e2=e1:Clone()
		e2:SetCode(EFFECT_UPDATE_DEFENCE)
		tc:RegisterEffect(e2)
		tc=g:GetNext()
	end
end